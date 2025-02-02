using Anvil.Services;
using XM.Plugin.Area.Entity;
using XM.Shared.API.NWNX.PlayerPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;

namespace XM.Plugin.Area.Map
{
    [ServiceBinding(typeof(MapProgressionService))]
    internal class MapProgressionService
    {
        private const string AreaProgressionLoadedPrefix = "AREA_PROGRESSION_LOADED_";

        private readonly XMEventService _event;
        private readonly DBService _db;

        public MapProgressionService(
            XMEventService @event,
            DBService db)
        {
            _event = @event;
            _db = db;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerLeave>(OnPlayerLeave);
            _event.Subscribe<AreaEvent.OnAreaEnter>(OnAreaEnter);
            _event.Subscribe<AreaEvent.OnAreaExit>(OnAreaExit);
        }

        private void OnAreaEnter(uint area)
        {
            var player = GetEnteringObject();
            LoadMapProgression(player, area);
        }

        private void OnPlayerLeave(uint module)
        {
            var player = GetExitingObject();
            var area = GetArea(player);
            SaveMapProgression(player, area);
        }

        private void OnAreaExit(uint area)
        {
            var player = GetExitingObject();
            SaveMapProgression(player, area);
        }

        private void SaveMapProgression(uint player, uint area)
        {
            if (!GetIsPC(player) || GetIsDM(player))
                return;

            var playerId = PlayerId.Get(player);
            var dbPlayerMap = _db.Get<PlayerMap>(playerId);
            var areaResref = GetResRef(area);

            if (string.IsNullOrWhiteSpace(areaResref)) 
                return;

            var progression = PlayerPlugin.GetAreaExplorationState(player, area);

            dbPlayerMap.MapProgressions[areaResref] = progression;

            _db.Set(dbPlayerMap);
        }

        private void LoadMapProgression(uint player, uint area)
        {
            if (!GetIsPC(player) || GetIsDM(player))
                return;

            var areaResref = GetResRef(area);
            var varName = AreaProgressionLoadedPrefix + areaResref;
            if (GetLocalBool(player, varName))
                return;

            var playerId = PlayerId.Get(player);
            var dbPlayerMap = _db.Get<PlayerMap>(playerId);

            if (!dbPlayerMap.MapProgressions.ContainsKey(areaResref))
                return;

            var progression = dbPlayerMap.MapProgressions[areaResref];
            PlayerPlugin.SetAreaExplorationState(player, area, progression);
            SetLocalBool(player, varName, true);
        }
    }
}
