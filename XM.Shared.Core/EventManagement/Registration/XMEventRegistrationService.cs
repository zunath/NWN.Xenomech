﻿using System;
using Anvil.Services;
using NLog;
using XM.Shared.API.NWNX.UtilPlugin;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;

namespace XM.Shared.Core.EventManagement.Registration
{
    [ServiceBinding(typeof(XMEventRegistrationService))]
    internal class XMEventRegistrationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly DBService _db;
        private readonly SchedulerService _scheduler;
        private readonly XMEventService _event;

        public XMEventRegistrationService(
            DBService db,
            SchedulerService scheduler,
            XMEventService @event)
        {
            _db = db;
            _scheduler = scheduler;
            _event = @event;

            RegisterEvents();
            SubscribeEvents();
            ScheduleXMHeartbeatEvent();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<XMEvent.OnServerHeartbeat>(EventScript.OnXMServerHeartbeatScript);
            _event.RegisterEvent<XMEvent.OnSpawnCreated>(EventScript.OnXMSpawnCreatedScript);
            _event.RegisterEvent<XMEvent.OnAreaCreated>(EventScript.OnXMAreaCreatedScript);
            _event.RegisterEvent<XMEvent.OnModuleContentChanged>(EventScript.OnXMModuleChangedScript);
            _event.RegisterEvent<XMEvent.OnDatabaseLoaded>(EventScript.OnXMDatabaseLoadedScript);
            _event.RegisterEvent<XMEvent.OnPCInitialized>(EventScript.OnXMPCInitializedScript);
            _event.RegisterEvent<XMEvent.OnPlayerMigrationBefore>(EventScript.OnXMPlayerMigrationBeforeScript);
            _event.RegisterEvent<XMEvent.OnPlayerMigrationAfter>(EventScript.OnXMPlayerMigrationAfterScript);
            _event.RegisterEvent<XMEvent.OnPlayerOpenAppearanceMenu>(EventScript.OnXMPlayerOpenedAppearanceMenuScript);
            _event.RegisterEvent<XMEvent.OnPlayerOpenQuestsMenu>(EventScript.OnXMPlayerOpenedQuestsMenuScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<NWNXEvent.OnModulePreload>(OnModulePreload);
        }

        private void ScheduleXMHeartbeatEvent()
        {
            _scheduler.ScheduleRepeating(() =>
            {
                _event.ExecuteScript(EventScript.OnXMServerHeartbeatScript, GetModule());
            }, TimeSpan.FromSeconds(6));
        }

        private void OnModulePreload(uint objectSelf)
        {
            DetermineContentChange();
        }

        private void DetermineContentChange()
        {
            var serverConfig = _db.Get<ModuleCache>(ModuleCache.CacheIdName) ?? new ModuleCache();

            if (UtilPlugin.GetModuleMTime() != serverConfig.LastModuleMTime)
            {
                _logger.Info("Module has changed since last boot. Running module changed event.");

                // DB record must be updated before the event fires, as some
                // events use the server configuration record.
                serverConfig.LastModuleMTime = UtilPlugin.GetModuleMTime();
                _db.Set(serverConfig);

                _event.ExecuteScript(EventScript.OnXMModuleChangedScript, GetModule());
            }
        }
    }
}
