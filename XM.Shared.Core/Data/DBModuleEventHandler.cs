using Anvil.Services;
using NLog;
using XM.Shared.Core.EventManagement;

namespace XM.Shared.Core.Data
{
    [ServiceBinding(typeof(DBModuleEventHandler))]
    public class DBModuleEventHandler
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly DBStartupService _startupService;
        private readonly XMEventService _event;

        public DBModuleEventHandler(DBStartupService startupService, XMEventService @event)
        {
            _startupService = startupService;
            _event = @event;
            
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnLoad>(OnModuleLoad);
        }

        private void OnModuleLoad(uint objectSelf)
        {
            _logger.Info("Module loaded, starting database initialization...");
            _ = _startupService.StartAsync();
        }
    }
} 