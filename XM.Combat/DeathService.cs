using Anvil.Services;
using NLog;
using XM.API.Constants;
using XM.Area;
using XM.Combat.Entity;
using XM.Core.EventManagement;
using XM.Core.EventManagement.ModuleEvent;
using XM.Core.EventManagement.XMEvent;
using XM.Data;
using XM.Localization;

namespace XM.Combat
{
    [ServiceBinding(typeof(DeathService))]
    internal class DeathService
    {
        private const string DefaultSpawnWaypointTag = "DTH_DEFAULT_RESPAWN_POINT";

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly DBService _db;
        private readonly AreaCacheService _areaCache;
        private readonly XMEventService _event;

        public DeathService(
            DBService db,
            AreaCacheService areaCache,
            XMEventService @event)
        {
            _db = db;
            _areaCache = areaCache;
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleOnPlayerDyingEvent>(OnModuleDying);
            _event.Subscribe<ModuleOnPlayerDeathEvent>(OnModuleDeath);
            _event.Subscribe<ModuleOnPlayerRespawnEvent>(OnModuleRespawn);
            _event.Subscribe<PCInitializedEvent>(OnPCInitialized);
        }


        private void OnModuleDying()
        {
            ApplyEffectToObject(DurationType.Instant, EffectDeath(), GetLastPlayerDying());
        }
        private void OnModuleDeath()
        {
            var player = GetLastPlayerDied();
            var hostile = GetLastHostileActor(player);

            SetStandardFactionReputation(StandardFactionType.Commoner, 100, player);
            SetStandardFactionReputation(StandardFactionType.Merchant, 100, player);
            SetStandardFactionReputation(StandardFactionType.Defender, 100, player);

            var factionMember = GetFirstFactionMember(hostile, false);
            while (GetIsObjectValid(factionMember))
            {
                ClearPersonalReputation(player, factionMember);
                factionMember = GetNextFactionMember(hostile, false);
            }

            PopUpDeathGUIPanel(player, true, true, 0, Locale.GetString(LocaleString.DeathPrompt));

            WriteDeathAudit(player);
        }

        private void OnModuleRespawn()
        {
            var player = GetLastRespawnButtonPresser();
            var maxHP = GetMaxHitPoints(player);

            var amount = maxHP / 2;
            ApplyEffectToObject(DurationType.Instant, EffectResurrection(), player);
            ApplyEffectToObject(DurationType.Instant, EffectHeal(amount), player);

            SendToHomePoint(player);
            WriteRespawnAudit(player);
        }

        /// <summary>
        /// Teleports player to his or her last home point.
        /// </summary>
        /// <param name="player">The player to teleport</param>
        private void SendToHomePoint(uint player)
        {
            var playerId = GetObjectUUID(player);
            var entity = _db.Get<PlayerRespawn>(playerId);
            var area = _areaCache.GetAreaByResref(entity.RespawnAreaResref);
            var position = Vector(
                entity.RespawnLocationX,
                entity.RespawnLocationY,
                entity.RespawnLocationZ);

            if (!GetIsObjectValid(area))
            {
                var defaultLocation = GetLocation(GetWaypointByTag(DefaultSpawnWaypointTag));
                AssignCommand(player, () => ActionJumpToLocation(defaultLocation));
            }
            else
            {
                var location = Location(area, position, entity.RespawnLocationOrientation);
                AssignCommand(player, () => ActionJumpToLocation(location));
            }
        }

        /// <summary>
        /// Writes an audit entry to the Death audit group.
        /// </summary>
        /// <param name="player">The player who respawned</param>
        private void WriteRespawnAudit(uint player)
        {
            var name = GetName(player);
            var log = $"RESPAWN - {name}";

            _logger.Info(log);
        }

        /// <summary>
        /// Write an audit entry with details of this death.
        /// </summary>
        /// <param name="player">The player who died</param>
        private static void WriteDeathAudit(uint player)
        {
            var name = GetName(player);
            var area = GetArea(player);
            var areaName = GetName(area);
            var areaTag = GetTag(area);
            var areaResref = GetResRef(area);
            var hostile = GetLastHostileActor(player);
            var hostileName = GetName(hostile);

            var log = $"DEATH: {name} - {areaName} - {areaTag} - {areaResref} Killed by: {hostileName}";
            _logger.Info(log);
        }

        public void OnPCInitialized()
        {
            var player = OBJECT_SELF;

            if (!GetIsPC(player) || GetIsDM(player)) 
                return;

            var playerId = GetObjectUUID(player);
            var dbPlayerRespawn = _db.Get<PlayerRespawn>(playerId) ?? new PlayerRespawn(playerId);

            var waypoint = GetWaypointByTag(DefaultSpawnWaypointTag);
            var position = GetPosition(waypoint);
            var areaResref = GetResRef(GetArea(waypoint));
            var facing = GetFacing(waypoint);

            dbPlayerRespawn.RespawnLocationX = position.X;
            dbPlayerRespawn.RespawnLocationY = position.Y;
            dbPlayerRespawn.RespawnLocationZ = position.Z;
            dbPlayerRespawn.RespawnAreaResref = areaResref;
            dbPlayerRespawn.RespawnLocationOrientation = facing;

            _db.Set(dbPlayerRespawn);
        }
    }
}
