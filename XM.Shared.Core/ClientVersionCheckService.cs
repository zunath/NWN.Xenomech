using Anvil.Services;
using NLog;
using NWN.Core.NWNX;
using System;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Shared.Core
{
    [ServiceBinding(typeof(ClientVersionCheckService))]
    internal class ClientVersionCheckService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly XMEventService _event;

        public ClientVersionCheckService(
            XMEventService @event)
        {
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<NWNXEvent.OnClientConnectBefore>(CheckClientVersion);
        }

        /// <summary>
        /// When a player connects to the server, perform a version check on their client.
        /// All of the NUI window features require version 8193.33 or higher but we restrict to 8193.34 or higher
        /// due to fixes applied in .34.
        /// </summary>
        private void CheckClientVersion(uint module)
        {
            const int RequiredMajorVersion = 8193;
            const int RequiredMinorVersion = 34;

            var majorVersion = Convert.ToInt32(EventsPlugin.GetEventData("VERSION_MAJOR"));
            var minorVersion = Convert.ToInt32(EventsPlugin.GetEventData("VERSION_MINOR"));

            // Version requirements are met.
            if (majorVersion > RequiredMajorVersion || (majorVersion == RequiredMajorVersion && minorVersion >= RequiredMinorVersion))
                return;

            // Version requirements are not met. Cancel the connection event and provide a reason why as well as instructions to the player on what to do.
            var playerName = EventsPlugin.GetEventData("PLAYER_NAME");
            var cdKey = EventsPlugin.GetEventData("CDKEY");
            var ipAddress = EventsPlugin.GetEventData("IP_ADDRESS");
            var platformId = EventsPlugin.GetEventData("PLATFORM_ID");

            var log = LocaleString.ClientVersionLogMessage.ToLocalizedString(playerName, cdKey, ipAddress, platformId, majorVersion, minorVersion);
            _logger.Info(log);

            var error = LocaleString.ClientVersionErrorMessageToPlayer.ToLocalizedString(RequiredMajorVersion, RequiredMinorVersion);
            EventsPlugin.SetEventResult(error);
            EventsPlugin.SkipEvent();
        }
    }
}
