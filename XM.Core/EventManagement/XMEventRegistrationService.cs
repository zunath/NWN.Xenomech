using System;
using System.Collections.Generic;
using Anvil.Services;
using NLog;
using XM.API.NWNX.UtilPlugin;
using XM.Core.Entity;
using XM.Core.EventManagement.NWNXEvent;
using XM.Core.EventManagement.XMEvent;
using XM.Data;

namespace XM.Core.EventManagement
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
            _event.RegisterEvent<ServerHeartbeatEvent>(EventScript.OnXMServerHeartbeatScript);
            _event.RegisterEvent<SpawnCreatedEvent>(EventScript.OnXMSpawnCreatedScript);
            _event.RegisterEvent<AreaCreatedEvent>(EventScript.OnXMAreaCreatedScript);
            _event.RegisterEvent<ModuleContentChangedEvent>(EventScript.OnXMModuleChangedScript);
            _event.RegisterEvent<CacheDataBeforeEvent>(EventScript.OnXMCacheDataBeforeScript);
            _event.RegisterEvent<CacheDataAfterEvent>(EventScript.OnXMCacheDataAfterScript);
            _event.RegisterEvent<DatabaseLoadedEvent>(EventScript.OnXMDatabaseLoadedScript);
            _event.RegisterEvent<PCInitializedEvent>(EventScript.OnXMPCInitializedScript);
            _event.RegisterEvent<PlayerMigrationBeforeEvent>(EventScript.OnXMPlayerMigrationBeforeScript);
            _event.RegisterEvent<PlayerMigrationAfterEvent>(EventScript.OnXMPlayerMigrationAfterScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModulePreloadEvent>(OnModulePreload);
        }

        private void ScheduleXMHeartbeatEvent()
        {
            _scheduler.ScheduleRepeating(() =>
            {
                ExecuteScript(EventScript.OnXMServerHeartbeatScript, GetModule());
            }, TimeSpan.FromSeconds(6));
        }

        private void OnModulePreload()
        {
            DetermineContentChange();
            ExecuteScript(EventScript.OnXMCacheDataBeforeScript);
            ExecuteScript(EventScript.OnXMCacheDataAfterScript);
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

                ExecuteScript(EventScript.OnXMModuleChangedScript, GetModule());
            }
        }
    }
}
