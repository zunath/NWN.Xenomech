using System.Threading.Tasks;
using Anvil.Services;
using NLog;

namespace XM.Shared.Core.Data
{
    [ServiceBinding(typeof(DBStartupService))]
    public class DBStartupService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly DBInitializationService _initService;

        public DBStartupService(DBInitializationService initService)
        {
            _initService = initService;
        }

        public async Task StartAsync()
        {
            _logger.Info("Starting database initialization in background...");
            
            // Start initialization in background to avoid blocking hot reloading
            _ = Task.Run(async () =>
            {
                try
                {
                    await _initService.InitializeAsync();
                }
                catch (System.Exception ex)
                {
                    _logger.Error(ex, "Failed to initialize database");
                }
            });
        }
    }
} 