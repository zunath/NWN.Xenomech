﻿using System.Linq;
using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using NLog;
using XM.Authorization.Entity;
using XM.Configuration;
using XM.Data;
using XM.Localization;

namespace XM.Authorization
{
    [ServiceBinding(typeof(AuthorizationService))]
    public class AuthorizationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly DBService _db;
        private readonly XMSettingsService _settings;

        public AuthorizationService(
            DBService db, 
            XMSettingsService settings)
        {
            _db = db;
            _settings = settings;

            HookEvents();
        }

        private void HookEvents()
        {
            NwModule.Instance.OnClientEnter += OnModuleEnter;
        }

        private void OnModuleEnter(ModuleEvents.OnClientEnter obj)
        {
            var dm = GetEnteringObject();
            if (!GetIsDM(dm) && !GetIsDMPossessed(dm)) 
                return;

            var authorizationLevel = GetAuthorizationLevel(dm);

            if (authorizationLevel != AuthorizationLevel.DM &&
                authorizationLevel != AuthorizationLevel.Admin)
            {
                LogDMAuthorization(false);
                BootPC(dm, Locale.GetString(LocaleStrings.NotAuthorizedToLogin));
                return;
            }

            LogDMAuthorization(true);
            ExecuteScript("dmfi_onclienter", OBJECT_SELF);
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

            var query = new DBQuery<AuthorizedDM>()
                .AddFieldSearch(nameof(AuthorizedDM.CDKey), cdKey, false);
            var existing = _db.Search(query).FirstOrDefault();
            if (existing == null)
                return AuthorizationLevel.Player;

            return existing.Authorization;
        }
    }
}