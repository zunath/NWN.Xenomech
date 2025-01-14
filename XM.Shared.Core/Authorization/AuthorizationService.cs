using System.Linq;
using Anvil.Services;
using NLog;
using XM.Shared.Core.Authorization.Entity;
using XM.Shared.Core.Configuration;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Shared.Core.Authorization
{
    [ServiceBinding(typeof(AuthorizationService))]
    public class AuthorizationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly DBService _db;
        private readonly XMSettingsService _settings;
        private readonly XMEventService _event;

        public AuthorizationService(
            DBService db, 
            XMSettingsService settings,
            XMEventService @event)
        {
            _db = db;
            _settings = settings;
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(OnPlayerEnter);
        }

        private void OnPlayerEnter(uint objectSelf)
        {
            var dm = GetEnteringObject();
            if (!GetIsDM(dm) && !GetIsDMPossessed(dm)) 
                return;

            var authorizationLevel = GetAuthorizationLevel(dm);

            if (authorizationLevel != AuthorizationLevel.DM &&
                authorizationLevel != AuthorizationLevel.Admin)
            {
                LogDMAuthorization(false);
                BootPC(dm, Locale.GetString(LocaleString.NotAuthorizedToLogin));
                return;
            }

            LogDMAuthorization(true);
            _event.ExecuteScript("dmfi_onclienter", OBJECT_SELF);
        }

        /// <summary>
        /// Logs whether an authorization attempt was successful.
        /// </summary>
        /// <param name="success">if true, will be logged as a successful attempt. if false, will be logged as unsuccessful.</param>
        private static void LogDMAuthorization(bool success)
        {
            var player = GetEnteringObject();
            var ipAddress = GetPCIPAddress(player);
            var cdKey = GetPCPublicCDKey(player);
            var account = GetPCPlayerName(player);
            var pcName = GetName(player);

            if (success)
            {
                var log = $"{pcName} - {account} - {cdKey} - {ipAddress}: Authorization successful";
                _logger.Info(log);
            }
            else
            {
                var log = $"{pcName} - {account} - {cdKey} - {ipAddress}: Authorization UNSUCCESSFUL";
                _logger.Info(log);
            }
        }

        /// <summary>
        /// Retrieves the authorization level of a given player.
        /// </summary>
        /// <param name="player">The player whose authorization level we're checking</param>
        /// <returns>The authorization level (player, DM, or admin)</returns>
        public AuthorizationLevel GetAuthorizationLevel(uint player)
        {
            var cdKey = GetPCPublicCDKey(player);

            if (!string.IsNullOrWhiteSpace(_settings.SuperAdminCDKey))
            {
                if (cdKey == _settings.SuperAdminCDKey)
                    return AuthorizationLevel.Admin;
            }

            var query = new DBQuery<IDBEntity>()
                .AddFieldSearch(nameof(AuthorizedDM.CDKey), cdKey, false);
            var existing = _db.Search<AuthorizedDM>(query).FirstOrDefault();
            if (existing == null)
                return AuthorizationLevel.Player;

            return existing.Authorization;
        }
    }
}
