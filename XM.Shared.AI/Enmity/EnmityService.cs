using Anvil.Services;
using System.Collections.Generic;
using System.Linq;
using XM.Progression.Stat;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Party;

namespace XM.AI.Enmity
{
    [ServiceBinding(typeof(EnmityService))]
    internal class EnmityService
    {
        // Enemy -> Creature -> EnmityAmount mapping
        private readonly Dictionary<uint, Dictionary<uint, CreatureEnmity>> _enemyEnmityTables = new();

        // Creature -> EnemyList mapping
        private readonly Dictionary<uint, List<uint>> _creatureToEnemies = new();

        private readonly EnmityLevelModifier _levelModifiers = new();

        private readonly XMEventService _event;
        private readonly PartyService _party;
        private readonly StatService _stat;

        public EnmityService(
            XMEventService @event, 
            PartyService party,
            StatService stat)
        {
            _event = @event;
            _party = party;
            _stat = stat;

            _event.Subscribe<CreatureEvent.OnDamaged>(OnDamaged);
            _event.Subscribe<CreatureEvent.OnMeleeAttacked>(OnAttacked);
            _event.Subscribe<CreatureEvent.OnDeath>(OnDeath);
            _event.Subscribe<ModuleEvent.OnPlayerDeath>(OnPlayerDeath);
            _event.Subscribe<ModuleEvent.OnPlayerLeave>(OnPlayerExit);
            _event.Subscribe<AreaEvent.OnAreaExit>(OnPlayerExit);
        }

        private void OnPlayerExit(uint obj)
        {
            var player = GetExitingObject();
            RemoveCreatureEnmity(player);
        }

        private void OnPlayerDeath(uint obj)
        {
            var player = GetLastPlayerDied();
            RemoveCreatureEnmity(player);
        }

        private void OnDeath(uint enemy)
        {
            ClearEnmityTables(enemy);
            RemoveCreatureEnmity(enemy);
        }

        private void OnAttacked(uint enemy)
        {
            var attacker = GetLastAttacker(enemy);

            ModifyEnmity(attacker, enemy, EnmityType.Volatile, 1);
            ModifyEnmity(attacker, enemy, EnmityType.Cumulative, 1);
        }

        private void OnDamaged(uint enemy)
        {
            var npcStats = _stat.GetNPCStats(enemy);
            var damager = GetLastDamager(enemy);
            var damage = GetTotalDamageDealt();
            var targetLevel = npcStats.Level; 

            var cumulative = CalculateCumulativeEnmityGain(targetLevel, damage);
            var @volatile = cumulative * 3;

            ModifyEnmity(damager, enemy, EnmityType.Cumulative, cumulative);
            ModifyEnmity(damager, enemy, EnmityType.Volatile, @volatile);
        }

        private int CalculateCumulativeEnmityGain(int targetLevel, int damageDealt)
        {
            // todo: specific enmity calculations based on attacker's level and DMG ratings
            // todo: source: https://www.bg-wiki.com/ffxi/Enmity
            var scalingFactor = _levelModifiers.GetModifier(targetLevel);
            return (int)(scalingFactor * 80 * damageDealt);
        }

        internal Dictionary<uint, CreatureEnmity> GetEnmityTable(uint enemy)
        {
            if (!_enemyEnmityTables.ContainsKey(enemy))
                return new Dictionary<uint, CreatureEnmity>();

            return _enemyEnmityTables[enemy]
                .ToDictionary(x => x.Key, y => y.Value);
        }

        public uint GetHighestEnmityTarget(uint enemy)
        {
            var enmityTable = GetEnmityTable(enemy);
            var target = enmityTable.Count <= 0
                ? OBJECT_INVALID
                : enmityTable.MaxBy(e => e.Value.TotalEnmity).Key;

            return target;
        }

        public void ModifyEnmity(uint creature, uint enemy, EnmityType type, int amount)
        {
            if (GetIsPC(enemy))
                return;

            // Enmity shouldn't matter if you're dead.
            if (GetIsDead(creature) || GetIsDead(enemy))
                return;

            // Players cannot be placed on an enmity table against each other.
            if (GetIsPC(creature) && GetIsPC(enemy))
                return;

            // Party members (droids, pets, associates) cannot gain enmity
            if (_party.IsInParty(creature, enemy))
                return;

            // Player associates cannot gain enmity towards each other
            if (GetIsPC(GetMaster(creature)) && GetIsPC(GetMaster(enemy)))
                return;

            // Value is zero, no action necessary.
            if (amount == 0) return;

            // Retrieve the creature's list of associated enemies.
            var enemyList = _creatureToEnemies.ContainsKey(creature) ? _creatureToEnemies[creature] : new List<uint>();

            // Enemy isn't on the creature's list. Add it now.
            if (!enemyList.Contains(enemy))
                enemyList.Add(enemy);

            // Enemy doesn't have any tables yet.
            if (!_enemyEnmityTables.ContainsKey(enemy))
                _enemyEnmityTables[enemy] = new Dictionary<uint, CreatureEnmity>();

            // This creature doesn't exist on the enemy's table yet.
            if (!_enemyEnmityTables[enemy].ContainsKey(creature))
                _enemyEnmityTables[enemy][creature] = new CreatureEnmity();

            // Modify the enemy's enmity toward this creature.
            int enmityValue;

            if (type == EnmityType.Cumulative)
                enmityValue = _enemyEnmityTables[enemy][creature].CumulativeEnmity + amount;
            else
                enmityValue = _enemyEnmityTables[enemy][creature].VolatileEnmity + amount;

            // Enmity cannot fall below 1.
            if (enmityValue < 1)
                enmityValue = 1;

            // Update the enemy's enmity toward this creature.
            if (type == EnmityType.Cumulative)
                _enemyEnmityTables[enemy][creature].CumulativeEnmity = enmityValue;
            else
                _enemyEnmityTables[enemy][creature].VolatileEnmity = enmityValue;

            // Update this creature's list of enemies.
            _creatureToEnemies[creature] = enemyList;
        }

        public void TickVolatileEnmity(uint enemy)
        {
            var table = GetEnmityTable(enemy);

            foreach (var (_, enmity) in table)
            {
                enmity.VolatileEnmity -= 80;
            }
        }

        private void ClearEnmityTables(uint enemy)
        {
            // Enemy isn't registered as having an enmity table.
            if (!_enemyEnmityTables.ContainsKey(enemy))
                return;

            // For every creature on this enemy's enmity table,
            // remove the enemy from that creature's list.
            var creatures = _enemyEnmityTables[enemy];
            foreach (var (creature, _) in creatures)
            {
                _creatureToEnemies[creature].Remove(enemy);
                if (_creatureToEnemies[creature].Count <= 0)
                {
                    _creatureToEnemies.Remove(creature);
                }
            }

            _enemyEnmityTables.Remove(enemy);
        }

        private void RemoveCreatureEnmity(uint creature)
        {
            // Creature isn't on any enmity table.
            if (!_creatureToEnemies.ContainsKey(creature)) return;

            // Retrieve all of the creatures who have this creature on their enmity table.
            var enemies = _creatureToEnemies[creature];
            foreach (var enemy in enemies)
            {
                _enemyEnmityTables[enemy].Remove(creature);
            }

            // Remove this creature from the targetToCreatures cache.
            _creatureToEnemies.Remove(creature);
        }
    }
}
