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
    internal class AreaEventRegistrationService: EventRegistrationServiceBase, IXMOnAreaCreated
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


        [ScriptHandler(EventScript.AreaOnEnterScript)]
        public void HandleOnAreaEnter() => HandleEvent(OnAreaEnterSubscriptions, (subscription) => subscription.OnAreaEnter());

        [ScriptHandler(EventScript.AreaOnExitScript)]
        public void HandleOnAreaExit() => HandleEvent(OnAreaExitSubscriptions, (subscription) => subscription.OnAreaExit());

        [ScriptHandler(EventScript.AreaOnHeartbeatScript)]
        public void HandleOnAreaHeartbeat() => HandleEvent(OnAreaHeartbeatSubscriptions, (subscription) => subscription.OnAreaHeartbeat());

        [ScriptHandler(EventScript.AreaOnUserDefinedEventScript)]
        public void HandleOnAreaUserDefinedEvent() => HandleEvent(OnAreaUserDefinedEventSubscriptions, (subscription) => subscription.OnAreaUserDefinedEvent());

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
            _logger.Info($"Registering area events...");
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
    }
}
