using System;
using System.Collections.Generic;
using Anvil.Services;
using NLog;

namespace XM.Shared.Core.EventManagement
{
    [ServiceBinding(typeof(XMEventService))]
    [ServiceBinding(typeof(IScriptDispatcher))]
    public class XMEventService: IScriptDispatcher
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<string, Type> _scriptToTypes = new();
        private readonly Dictionary<Type, Dictionary<Guid, Action<uint>>> _scriptActionRegistrations = new();

        public void RegisterEvent<T>(string scriptName)
            where T : IXMEvent
        {
            if (!_scriptToTypes.ContainsKey(scriptName))
            {
                _scriptToTypes[scriptName] = typeof(T);
            }
        }

        public Guid Subscribe<T>(Action<uint> action)
            where T: IXMEvent
        {
            var type = typeof(T);
            if (!_scriptActionRegistrations.ContainsKey(type))
            {
                _scriptActionRegistrations[type] = new Dictionary<Guid, Action<uint>>();
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
                return;

            _scriptActionRegistrations[type].Remove(id);
        }

        private void RunEventHandlers(uint objectSelf, Type type)
        {
            if (!_scriptActionRegistrations.ContainsKey(type))
                return;

            foreach (var (_, action) in _scriptActionRegistrations[type])
            {
                try
                {
                    action(objectSelf);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        public ScriptHandleResult ExecuteScript(string scriptName, uint objectSelf)
        {
            if (!_scriptToTypes.ContainsKey(scriptName)) 
                return ScriptHandleResult.NotHandled;

            var type = _scriptToTypes[scriptName];
            RunEventHandlers(objectSelf, type);
            return ScriptHandleResult.Handled;
        }

        public int ExecutionOrder => 0;
    }
}
