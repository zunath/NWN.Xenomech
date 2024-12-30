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
        public IList<IXMOnServerHeartbeat> OnXMServerHeartbeatSubscriptions { get; set; }

        [Inject]
        public IList<IXMOnSpawnCreated> OnXMSpawnCreatedSubscriptions { get; set; }

        [Inject]
        public IList<IXMOnAreaCreated> OnXMAreaCreatedSubscriptions { get; set; }

        [Inject]
        public IList<IXMOnModuleContentChanged> OnXMModuleContentChangedSubscriptions { get; set; }


        [ScriptHandler(EventScript.OnXMServerHeartbeatScript)]
        public void HandleXMServerHeartbeat() => HandleEvent(OnXMServerHeartbeatSubscriptions, (subscription) => subscription.OnXMServerHeartbeat());

        [ScriptHandler(EventScript.OnXMSpawnCreatedScript)]
        public void HandleXMSpawnCreated() => HandleEvent(OnXMSpawnCreatedSubscriptions, (subscription) => subscription.OnSpawnCreated());

        [ScriptHandler(EventScript.OnXMAreaCreatedScript)]
        public void HandleXMAreaCreated() => HandleEvent(OnXMAreaCreatedSubscriptions, (subscription) => subscription.OnAreaCreated());

        [ScriptHandler(EventScript.OnXMModuleChangedScript)]
        public void HandleModuleChanged() => HandleEvent(OnXMModuleContentChangedSubscriptions, (subscription) => subscription.OnModuleContentChanged());


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
