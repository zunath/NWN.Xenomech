using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.API;
using Anvil.Services;
using NLog;
using NWN.Core.NWNX;

namespace XM.Shared.Core.EventManagement
{
    [ServiceBinding(typeof(XMEventService))]
    public class XMEventService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<string, Type> _scriptToTypes = new();
        private readonly Dictionary<Type, Dictionary<Guid, Action<uint>>> _scriptActionRegistrations = new();
        private readonly ScriptHandleFactory _factory;

        public XMEventService(ScriptHandleFactory factory)
        {
            _factory = factory;
        }

        public void RegisterEvent<T>(string scriptName)
            where T : IXMEvent
        {
            if (!_scriptToTypes.ContainsKey(scriptName))
            {
                var type = typeof(T);
                _scriptToTypes[scriptName] = type;

                _factory.RegisterScriptHandler(scriptName, HandleNWNScript);

                EventsPlugin.SubscribeEvent(type.FullName!, scriptName);
            }
        }

        private ScriptHandleResult HandleNWNScript(CallInfo arg)
        {
            if (!_scriptToTypes.ContainsKey(arg.ScriptName))
                return ScriptHandleResult.NotHandled;

            var type = _scriptToTypes[arg.ScriptName];
            RunEventHandlers(arg.ObjectSelf, type);
            return ScriptHandleResult.Handled;
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

            for(var x = 0; x <= _scriptActionRegistrations[type].Count-1; x++)
            {
                var (_, action) = _scriptActionRegistrations[type].ElementAt(x);
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

        private readonly Dictionary<string, object> _eventData = new();

        public void PublishEvent<T>(uint target, T data = default)
            where T : IXMEvent
        {
            var type = typeof(T);
            var eventId = Guid.NewGuid().ToString();

            if(data != null)
                EventsPlugin.PushEventData("EVENT_DATA_ID", eventId);

            _eventData[eventId] = data;
            EventsPlugin.SignalEvent(type.FullName!, target);
            _eventData.Remove(eventId);
        }

        public T GetEventData<T>()
            where T : IXMEvent
        {
            var eventId = EventsPlugin.GetEventData("EVENT_DATA_ID");
            var data = _eventData[eventId];

            return (T)data;
        }

        public int ExecutionOrder => 0;
    }
}
