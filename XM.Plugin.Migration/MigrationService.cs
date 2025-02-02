using Anvil.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using XM.Migration.Entity;
using NLog;
using XM.Shared.API.NWNX.AdminPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;

namespace XM.Migration
{
    [ServiceBinding(typeof(MigrationService))]
    [ServiceBinding(typeof(IInitializable))]
    internal class MigrationService: IInitializable
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private int _currentMigrationVersion;
        private int _newMigrationVersion;
        private readonly Dictionary<int, IServerMigration> _serverMigrations = new();
        private readonly Dictionary<int, IPlayerMigration> _playerMigrations = new();

        private readonly DBService _db;
        private readonly XMEventService _event;

        public MigrationService(DBService db, XMEventService @event)
        {
            _db = db;
            _event = @event;

            @event.Subscribe<ModuleEvent.OnPlayerEnter>(OnModuleEnter);
        }

        private void OnModuleEnter(uint objectSelf)
        {
            RunPlayerMigrations();
        }

        private void UpdateMigrationVersion()
        {
            if (_newMigrationVersion > _currentMigrationVersion)
            {
                var config = GetServerConfiguration();
                config.MigrationVersion = _newMigrationVersion;
                _db.Set(config);
            }
        }

        private ServerMigrationStatus GetServerConfiguration()
        {
            return _db.Get<ServerMigrationStatus>(ServerMigrationStatus.MigrationIdName) ?? new ServerMigrationStatus();
        }

        private IEnumerable<IServerMigration> GetMigrations()
        {
            var serverConfig = GetServerConfiguration();
            var migrationVersion = serverConfig.MigrationVersion;
            var migrations = _serverMigrations
                .Where(x => x.Key > migrationVersion)
                .OrderBy(o => o.Key)
                .Select(s => s.Value);

            return migrations;
        }

        private void RunMigrations()
        {
            var sw = new Stopwatch();
            var migrations = GetMigrations();
            var newVersion = 0;

            foreach (var migration in migrations)
            {
                sw.Reset();
                try
                {
                    sw.Start();
                    migration.Migrate();
                    newVersion = migration.Version;
                    sw.Stop();
                    _logger.Info($"Server migration #{migration.Version} completed successfully. (Took {sw.ElapsedMilliseconds}ms)");
                }
                catch (Exception ex)
                {
                    // It's dangerous to proceed without a successful migration. Shut down the server in this situation.
                    _logger.Error(ex, $"Server migration #{migration.Version} failed to apply. Shutting down server.");
                    AdminPlugin.ShutdownServer();
                    break;
                }
            }

            if (_newMigrationVersion < newVersion)
                _newMigrationVersion = newVersion;
        }

        private void RunServerMigrations()
        {
            RunMigrations();
        }

        /// <summary>
        /// When a player logs into the server and after initialization has run, run the migration process on their character.
        /// </summary>
        private void RunPlayerMigrations()
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player))
                return;

            var sw = new Stopwatch();
            var playerId = PlayerId.Get(player);
            var dbPlayerMigration = _db.Get<PlayerMigrationStatus>(playerId) ?? new PlayerMigrationStatus(playerId);

            var migrations = _playerMigrations
                .Where(x => x.Key > dbPlayerMigration.MigrationVersion)
                .OrderBy(o => o.Key)
                .Select(s => s.Value);
            var newVersion = dbPlayerMigration.MigrationVersion;

            foreach (var migration in migrations)
            {
                sw.Reset();
                try
                {
                    sw.Start();
                    migration.Migrate(player);
                    newVersion = migration.Version;
                    sw.Stop();
                    _logger.Info($"Player migration #{migration.Version} applied to player {GetName(player)} [{playerId}] successfully. (Took {sw.ElapsedMilliseconds}ms)");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"Player migration #{migration.Version} failed to apply for player {GetName(player)} [{playerId}]");
                    break;
                }
            }

            // Migrations can edit the database player entity. Refresh it before updating the version.
            dbPlayerMigration = _db.Get<PlayerMigrationStatus>(playerId) ?? new PlayerMigrationStatus(playerId);
            dbPlayerMigration.MigrationVersion = newVersion;
            _db.Set(dbPlayerMigration);

            _event.PublishEvent<XMEvent.OnPlayerMigrationAfter>(player);
        }

        private void LoadServerMigrations()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(w => typeof(IServerMigration).IsAssignableFrom(w) && !w.IsInterface && !w.IsAbstract);

            foreach (var type in types)
            {
                var instance = (IServerMigration)Activator.CreateInstance(type);

                _serverMigrations.Add(instance.Version, instance);
            }
        }

        private void LoadPlayerMigrations()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(w => typeof(IPlayerMigration).IsAssignableFrom(w) && !w.IsInterface && !w.IsAbstract);

            foreach (var type in types)
            {
                var instance = (IPlayerMigration)Activator.CreateInstance(type);

                _playerMigrations.Add(instance.Version, instance);
            }
        }

        /// <summary>
        /// Retrieves the latest migration version for players.
        /// </summary>
        /// <returns>The latest migration version for players.</returns>
        public int GetLatestPlayerVersion()
        {
            return _playerMigrations.Max(m => m.Key);
        }

        public void Init()
        {
            var config = GetServerConfiguration();
            _currentMigrationVersion = config.MigrationVersion;

            LoadServerMigrations();
            LoadPlayerMigrations();

            RunMigrations();
            UpdateMigrationVersion();
        }
    }
}
