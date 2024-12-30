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
    [ServiceBinding(typeof(INWNXOnModulePreload))]
    internal class XMEventRegistrationService: EventRegistrationServiceBase, INWNXOnModulePreload
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly DBService _db;
        private readonly SchedulerService _scheduler;

        [Inject]
        public IList<IXMOnServerHeartbeat> OnServerHeartbeatSubscriptions { get; set; }

        [Inject]
        public IList<IXMOnSpawnCreated> OnSpawnCreatedSubscriptions { get; set; }

        [Inject]
        public IList<IXMOnAreaCreated> OnAreaCreatedSubscriptions { get; set; }

        [Inject]
        public IList<IXMOnModuleContentChanged> OnModuleContentChangedSubscriptions { get; set; }

        [Inject]
        public IList<IXMOnCacheDataBefore> OnCacheDataBeforeSubscriptions { get; set; }

        [Inject]
        public IList<IXMOnCacheDataAfter> OnCacheDataAfterSubscriptions { get; set; }

        [Inject]
        public IList<IXMOnDatabaseLoaded> OnDatabaseLoadedSubscriptions { get; set; }

        [Inject]
        public IList<IXMOnPCInitialized> OnPCInitializedSubscriptions { get; set; }

        [Inject]
        public IList<IXMPlayerMigrationBefore> OnPlayerMigrationBeforeSubscriptions { get; set; }
        [Inject]
        public IList<IXMPlayerMigrationAfter> OnPlayerMigrationAfterSubscriptions { get; set; }


        [ScriptHandler(EventScript.OnXMServerHeartbeatScript)]
        public void HandleXMServerHeartbeat() => HandleEvent(OnServerHeartbeatSubscriptions, (subscription) => subscription.OnXMServerHeartbeat());

        [ScriptHandler(EventScript.OnXMSpawnCreatedScript)]
        public void HandleXMSpawnCreated() => HandleEvent(OnSpawnCreatedSubscriptions, (subscription) => subscription.OnSpawnCreated());

        [ScriptHandler(EventScript.OnXMAreaCreatedScript)]
        public void HandleXMAreaCreated() => HandleEvent(OnAreaCreatedSubscriptions, (subscription) => subscription.OnAreaCreated());

        [ScriptHandler(EventScript.OnXMModuleChangedScript)]
        public void HandleModuleChanged() => HandleEvent(OnModuleContentChangedSubscriptions, (subscription) => subscription.OnModuleContentChanged());

        [ScriptHandler(EventScript.OnXMCacheDataBeforeScript)]
        public void HandleCacheDataBefore() => HandleEvent(OnCacheDataBeforeSubscriptions, (subscription) => subscription.OnCacheDataBefore());

        [ScriptHandler(EventScript.OnXMCacheDataAfterScript)]
        public void HandleCacheDataAfter() => HandleEvent(OnCacheDataAfterSubscriptions, (subscription) => subscription.OnCacheDataAfter());

        [ScriptHandler(EventScript.OnXMDatabaseLoadedScript)]
        public void HandleDatabaseLoaded() => HandleEvent(OnDatabaseLoadedSubscriptions, (subscription) => subscription.OnDatabaseLoaded());

        [ScriptHandler(EventScript.OnXMPCInitializedScript)]
        public void HandlePlayerInitialized() => HandleEvent(OnPCInitializedSubscriptions, (subscription) => subscription.OnPCInitialized());

        [ScriptHandler(EventScript.OnXMPlayerMigrationBeforeScript)]
        public void HandlePlayerMigrationBefore() => HandleEvent(OnPlayerMigrationBeforeSubscriptions, (subscription) => subscription.OnPlayerMigrationBefore());

        [ScriptHandler(EventScript.OnXMPlayerMigrationAfterScript)]
        public void HandlePlayerMigrationAfter() => HandleEvent(OnPlayerMigrationAfterSubscriptions, (subscription) => subscription.OnPlayerMigrationAfter());

        public XMEventRegistrationService(
            DBService db,
            SchedulerService scheduler)
        {
            _db = db;
            _scheduler = scheduler;

            ScheduleXMHeartbeatEvent();
        }

        private void ScheduleXMHeartbeatEvent()
        {
            _scheduler.ScheduleRepeating(() =>
            {
                ExecuteScript(EventScript.OnXMServerHeartbeatScript, GetModule());
            }, TimeSpan.FromSeconds(6));
        }

        public void OnModulePreload()
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
