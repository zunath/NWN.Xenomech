using System;
using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using Action = System.Action;

namespace XM.Shared.Core.EventManagement
{
    [ServiceBinding(typeof(XMEventService))]
    public class XMEventService
    {
        private readonly Dictionary<string, Type> _scriptToTypes = new();
        private readonly Dictionary<Type, Dictionary<Guid, Action>> _scriptActionRegistrations = new();

        private readonly ScriptHandleFactory _scriptHandleFactory;

        public XMEventService(ScriptHandleFactory scriptHandleFactory)
        {
            _scriptHandleFactory = scriptHandleFactory;
        }

        public void RegisterEvent<T>(string scriptName)
            where T : IXMEvent
        {
            _scriptHandleFactory.RegisterScriptHandler(scriptName, HandleScript);

            if (!_scriptToTypes.ContainsKey(scriptName))
            {
                _scriptToTypes[scriptName] = typeof(T);
            }
        }

        private ScriptHandleResult HandleScript(CallInfo arg)
        {
            var type = _scriptToTypes[arg.ScriptName];

            RunEventHandlers(type);

            return ScriptHandleResult.Handled;
        }

        public Guid Subscribe<T>(Action action)
            where T: IXMEvent
        {
            var type = typeof(T);
            if (!_scriptActionRegistrations.ContainsKey(type))
            {
                _scriptActionRegistrations[type] = new Dictionary<Guid, Action>();
            }

            var id = Guid.NewGuid();
            _scriptActionRegistrations[type][id] = action;

            return id;
        }

        public void Unsubscribe<T>(Guid id)
            where T: IXMEvent
        {
            var type = typeof(T);
            if (!_scriptActionRegistrations.ContainsKey(type))
                throw new Exception($"Type {type} is unregistered.");

            if (!_scriptActionRegistrations[type].ContainsKey(id))
                throw new Exception($"Id '{id}' not found in registrations.");

            _scriptActionRegistrations[type].Remove(id);
        }

        private void RunEventHandlers(Type type)
        {
            if (!_scriptActionRegistrations.ContainsKey(type))
                return;

            foreach (var (_, action) in _scriptActionRegistrations[type])
            {
                action();
            }
        }
    }
}
