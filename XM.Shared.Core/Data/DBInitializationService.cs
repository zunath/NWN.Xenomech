using System.Threading.Tasks;
using Anvil.Services;
using NLog;

namespace XM.Shared.Core.Data
{
    [ServiceBinding(typeof(DBInitializationService))]
    public class DBInitializationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IntegratedDBService _dbService;

        public DBInitializationService(IntegratedDBService dbService)
        {
            _dbService = dbService;
        }

        public async Task InitializeAsync()
        {
            _logger.Info("Starting database initialization...");
            await _dbService.InitializeAsync();
            _logger.Info("Database initialization completed.");
        }
    }
} 