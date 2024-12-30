using System;
using System.Collections.Generic;
using Anvil.Services;
using NLog;
using XM.API.Constants;
using XM.Core.EventManagement.AreaEvent;
using XM.Core.EventManagement.XMEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(AreaEventRegistrationService))]
    internal class AreaEventRegistrationService: IXMOnAreaCreated
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IList<IOnAreaEnter> OnAreaEnterSubscriptions { get; set; }

        [Inject]
        public IList<IOnAreaExit> OnAreaExitSubscriptions { get; set; }

        [Inject]
        public IList<IOnAreaHeartbeat> OnAreaHeartbeatSubscriptions { get; set; }

        [Inject]
        public IList<IOnAreaUserDefinedEvent> OnAreaUserDefinedEventSubscriptions { get; set; }

        public AreaEventRegistrationService()
        {
            HookAreaEvents();
        }

        private void SetAreaScripts(uint area)
        {
            SetEventScript(area, EventScriptType.AreaOnEnter, EventScript.AreaOnEnterScript);
            SetEventScript(area, EventScriptType.AreaOnExit, EventScript.AreaOnExitScript);
            SetEventScript(area, EventScriptType.AreaOnHeartbeat, string.Empty); // Disabled for performance reasons
            SetEventScript(area, EventScriptType.AreaOnUserDefinedEvent, EventScript.AreaOnUserDefinedEventScript);
        }

        private void HookAreaEvents()
        {
            Console.WriteLine($"Registering area events...");
            for (var area = GetFirstArea(); GetIsObjectValid(area); area = GetNextArea())
            {
                SetAreaScripts(area);
            }
        }

        public void OnAreaCreated()
        {
            var area = OBJECT_SELF;
            SetAreaScripts(area);
        }

        [ScriptHandler(EventScript.AreaOnEnterScript)]
        public void HandleOnAreaEnter()
        {
            foreach (var handler in OnAreaEnterSubscriptions)
            {
                try
                {
                    handler.OnAreaEnter();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.AreaOnExitScript)]
        public void HandleOnAreaExit()
        {
            foreach (var handler in OnAreaExitSubscriptions)
            {
                try
                {
                    handler.OnAreaExit();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.AreaOnHeartbeatScript)]
        public void HandleOnAreaHeartbeat()
        {
            foreach (var handler in OnAreaHeartbeatSubscriptions)
            {
                try
                {
                    handler.OnAreaHeartbeat();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.AreaOnUserDefinedEventScript)]
        public void HandleOnAreaUserDefinedEvent()
        {
            foreach (var handler in OnAreaUserDefinedEventSubscriptions)
            {
                try
                {
                    handler.OnAreaUserDefinedEvent();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

    }
}
