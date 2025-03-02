using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.AI.Event;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Party;

namespace XM.AI.Enmity
{
    [ServiceBinding(typeof(EnmityService))]
    public class EnmityService
    {
        // Enemy -> Creature -> EnmityAmount mapping
        private readonly Dictionary<uint, Dictionary<uint, CreatureEnmity>> _enemyEnmityTables = new();

        // Creature -> EnemyList mapping
        private readonly Dictionary<uint, List<uint>> _creatureToEnemies = new();

        private readonly EnmityTables _enmityTables = new();

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

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<CreatureEvent.OnDamaged>(OnCreatureDamaged);
            _event.Subscribe<CreatureEvent.OnMeleeAttacked>(OnCreatureAttacked);
            _event.Subscribe<CreatureEvent.OnDeath>(OnCreatureDeath);
            _event.Subscribe<ModuleEvent.OnPlayerDeath>(OnPlayerDeath);
            _event.Subscribe<PlayerEvent.OnDamaged>(OnPlayerDamaged);
            _event.Subscribe<ModuleEvent.OnPlayerLeave>(OnPlayerExit);
            _event.Subscribe<AreaEvent.OnAreaExit>(OnPlayerExit);
            _event.Subscribe<AIEvent.OnEnterAggroAOE>(OnEnterAggroRange);
        }

        private void OnEnterAggroRange(uint aoe)
        {
            var npc = GetAreaOfEffectCreator(aoe);
            var entering = GetEnteringObject();
            var table = GetEnmityTable(npc);

            if (GetIsReactionTypeHostile(npc, entering) &&
                !table.ContainsKey(entering))
            {
                ModifyEnmity(entering, npc, EnmityType.Cumulative, 1);
            }
        }

        private void ReduceCumulativeEnmity(uint creature, uint enemy, int damage)
        {
            var maxHP = _stat.GetMaxHP(creature);
            var cumulativeLoss = (int)(1800 * damage / (float)maxHP);
            ModifyEnmity(creature, enemy, EnmityType.Cumulative, -cumulativeLoss);
        }

        private void OnPlayerDamaged(uint player)
        {
            var enemy = GetLastDamager(player);
            var damage = GetTotalDamageDealt();
            ReduceCumulativeEnmity(player, enemy, damage);
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

        private void OnCreatureDeath(uint enemy)
        {
            ClearEnmityTables(enemy);
            RemoveCreatureEnmity(enemy);
        }

        private void OnCreatureAttacked(uint enemy)
        {
            var attacker = GetLastAttacker(enemy);

            ModifyEnmity(attacker, enemy, EnmityType.Volatile, 1);
            ModifyEnmity(attacker, enemy, EnmityType.Cumulative, 1);
        }

        private void OnCreatureDamaged(uint creature)
        {
            var npcStats = _stat.GetNPCStats(creature);
            var enemy = GetLastDamager(creature);
            var damage = GetTotalDamageDealt();
            var targetLevel = npcStats.Level; 

            var cumulative = CalculateCumulativeDamageEnmityGain(targetLevel, damage);
            var @volatile = CalculateVolatileDamageEnmityGain(targetLevel, damage);

            ModifyEnmity(enemy, creature, EnmityType.Cumulative, cumulative);
            ModifyEnmity(enemy, creature, EnmityType.Volatile, @volatile);

            ReduceCumulativeEnmity(creature, enemy, damage);
        }

        // source: https://www.bg-wiki.com/ffxi/Enmity
        private int CalculateCumulativeDamageEnmityGain(int targetLevel, int damageDealt)
        {
            var scalingFactor = _enmityTables.GetCEDamageModifier(targetLevel);
            return (int)(scalingFactor * damageDealt);
        }

        private int CalculateVolatileDamageEnmityGain(int targetLevel, int damageDealt)
        {
            var scalingFactor = _enmityTables.GetVEDamageModifier(targetLevel);
            return (int)(scalingFactor * damageDealt);
        }

        private int CalculateCumulativeHealingEnmityGain(int targetLevel, int damageHealed)
        {
            var scalingFactor = _enmityTables.GetCEHealingModifier(targetLevel);
            return (int)(scalingFactor * damageHealed);
        }

        private int CalculateVolatileHealingEnmityGain(int targetLevel, int damageHealed)
        {
            var scalingFactor = _enmityTables.GetVEHealingModifier(targetLevel);
            return (int)(scalingFactor * damageHealed);
        }

        public void ApplyHealingEnmity(uint healer, int damageHealed)
        {
            if (!_creatureToEnemies.ContainsKey(healer))
                return;

            var enemies = _creatureToEnemies[healer];

            foreach (var enemy in enemies)
            {
                var npcStats = _stat.GetNPCStats(enemy);
                var level = npcStats.Level;

                var cumulative = CalculateCumulativeHealingEnmityGain(level, damageHealed);
                var @volatile = CalculateVolatileHealingEnmityGain(level, damageHealed);

                ModifyEnmity(healer, enemy, EnmityType.Cumulative, cumulative);
                ModifyEnmity(healer, enemy, EnmityType.Volatile, @volatile);
            }
        }

        internal Dictionary<uint, CreatureEnmity> GetEnmityTable(uint enemy)
        {
            if (!_enemyEnmityTables.ContainsKey(enemy))
                return new Dictionary<uint, CreatureEnmity>();

            return _enemyEnmityTables[enemy]
                .ToDictionary(x => x.Key, y => y.Value);
        }

        internal uint GetHighestEnmityTarget(uint enemy)
        {
            var enmityTable = GetEnmityTable(enemy);
            var target = enmityTable.Count <= 0
                ? OBJECT_INVALID
                : enmityTable.MaxBy(e => e.Value.TotalEnmity).Key;

            return target;
        }

        public void ModifyEnmity(uint creature, uint enemy, EnmityType type, int amount)
        {
            if (GetObjectType(creature) != ObjectType.Creature ||
                GetObjectType(enemy) != ObjectType.Creature)
                return;

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
            {
                var bonusEnmity = _stat.GetEnmityAdjustment(creature) * 0.01f;
                amount += (int)(amount * bonusEnmity);

                enmityValue = _enemyEnmityTables[enemy][creature].CumulativeEnmity + amount;
            }
            else
            {
                enmityValue = _enemyEnmityTables[enemy][creature].VolatileEnmity + amount;
            }

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

        public void ModifyEnmityOnAll(uint creature, EnmityType type, int amount)
        {
            // Value is zero, no action necessary.
            if (amount == 0) 
                return;

            // Creature has no enemies.
            if (!_creatureToEnemies.ContainsKey(creature)) 
                return;

            foreach (var enemy in _creatureToEnemies[creature])
            {
                ModifyEnmity(creature, enemy, type, amount);
            }
        }

        internal void TickVolatileEnmity(uint enemy)
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
        public bool HasEnmity(uint creature)
        {
            return _creatureToEnemies.ContainsKey(creature)
                   && _creatureToEnemies[creature].Count > 0;
        }
    }
}
