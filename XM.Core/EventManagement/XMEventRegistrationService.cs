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
    internal class XMEventRegistrationService: INWNXOnModulePreload
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
                Console.WriteLine("Module has changed since last boot. Running module changed event.");

                // DB record must be updated before the event fires, as some
                // events use the server configuration record.
                serverConfig.LastModuleMTime = UtilPlugin.GetModuleMTime();
                _db.Set(serverConfig);

                ExecuteScript(EventScript.OnXMModuleChangedScript, GetModule());
            }
        }

        [ScriptHandler(EventScript.OnXMServerHeartbeatScript)]
        public void HandleXMServerHeartbeat()
        {
            foreach (var handler in OnXMServerHeartbeatSubscriptions)
            {
                try
                {
                    handler.OnXMServerHeartbeat();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnXMSpawnCreatedScript)]
        public void HandleXMSpawnCreated()
        {
            foreach (var handler in OnXMSpawnCreatedSubscriptions)
            {
                try
                {
                    handler.OnSpawnCreated();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnXMAreaCreatedScript)]
        public void HandleXMAreaCreated()
        {
            foreach (var handler in OnXMAreaCreatedSubscriptions)
            {
                try
                {
                    handler.OnAreaCreated();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.OnXMModuleChangedScript)]
        public void HandleModuleChanged()
        {
            foreach (var handler in OnXMModuleContentChangedSubscriptions)
            {
                try
                {
                    handler.OnModuleContentChanged();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }
    }
}
