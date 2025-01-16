﻿using Anvil.Services;
using NLog;
using XM.Shared.API.Constants;

namespace XM.Shared.Core.EventManagement.Registration
{
    [ServiceBinding(typeof(AreaEventRegistrationService))]
    internal class AreaEventRegistrationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly XMEventService _event;

        public AreaEventRegistrationService(XMEventService @event)
        {
            _event = @event;

            _logger.Info($"Registering area events...");
            RegisterEvents();
            HookAreaScripts();
            SubscribeScripts();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<AreaEvent.OnAreaEnter>(EventScript.AreaOnEnterScript);
            _event.RegisterEvent<AreaEvent.OnAreaExit>(EventScript.AreaOnExitScript);
            _event.RegisterEvent<AreaEvent.OnAreaHeartbeat>(EventScript.AreaOnHeartbeatScript);
            _event.RegisterEvent<AreaEvent.OnAreaUserDefinedEvent>(EventScript.AreaOnUserDefinedEventScript);
        }

        private void HookAreaScripts()
        {
            for (var area = GetFirstArea(); GetIsObjectValid(area); area = GetNextArea())
            {
                SetAreaScripts(area);
            }
        }

        private void SetAreaScripts(uint area)
        {
            SetEventScript(area, EventScriptType.AreaOnEnter, EventScript.AreaOnEnterScript);
            SetEventScript(area, EventScriptType.AreaOnExit, EventScript.AreaOnExitScript);
            SetEventScript(area, EventScriptType.AreaOnHeartbeat, string.Empty); // Disabled for performance reasons
            SetEventScript(area, EventScriptType.AreaOnUserDefinedEvent, EventScript.AreaOnUserDefinedEventScript);
        }

        private void SubscribeScripts()
        {
            _event.Subscribe<XMEvent.OnAreaCreated>(OnAreaCreated);
        }

        public void OnAreaCreated(uint objectSelf)
        {
            var area = OBJECT_SELF;
            SetAreaScripts(area);
        }
    }
}
