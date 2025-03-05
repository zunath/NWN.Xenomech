using Anvil.Services;
using XM.Chat.Entity;
using XM.Shared.API.NWNX.RenamePlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Chat.Roleplay
{
    [ServiceBinding(typeof(PlayerNameService))]
    internal class PlayerNameService
    {
        private readonly XMEventService _event;
        private readonly DBService _db;

        private const string UnknownNamePrefix = "<c~~~>";
        private const string UnknownNameSuffix = "</c>";

        public PlayerNameService(
            XMEventService @event,
            DBService db)
        {
            _event = @event;
            _db = db;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(LoadNames);
        }

        private void LoadNames(uint module)
        {
            var player = GetEnteringObject();

            if (!GetIsPC(player) || GetIsDM(player))
                return;

            var anonymousName = LocaleString.Someone.ToLocalizedString();
            RenamePlugin.SetPCNameOverride(player, anonymousName, UnknownNamePrefix, UnknownNameSuffix, PlayerNameOverrideType.Default);
            RenamePlugin.SetPCNameOverride(player, GetName(player), string.Empty, string.Empty, PlayerNameOverrideType.Default, player);

            var playerId = PlayerId.Get(player);
            var dbPlayerName = _db.Get<PlayerName>(playerId);

            for (var otherPlayer = GetFirstPC(); GetIsObjectValid(otherPlayer); otherPlayer = GetNextPC())
            {
                if (!GetIsPC(otherPlayer) || GetIsDM(otherPlayer))
                    continue;

                var otherPlayerId = PlayerId.Get(otherPlayer);
                var dbOtherPlayerNames = _db.Get<PlayerName>(otherPlayerId);

                if (dbPlayerName.OverrideNames.ContainsKey(otherPlayerId))
                {
                    var name = dbPlayerName.OverrideNames[otherPlayerId];
                    RenamePlugin.SetPCNameOverride(otherPlayer, name, string.Empty, string.Empty, PlayerNameOverrideType.Default, player);
                }

                if (dbOtherPlayerNames.OverrideNames.ContainsKey(playerId))
                {
                    var name = dbOtherPlayerNames.OverrideNames[playerId];
                    RenamePlugin.SetPCNameOverride(player, name, string.Empty, string.Empty, PlayerNameOverrideType.Default, otherPlayer);
                }
            }
        }
    }
}
