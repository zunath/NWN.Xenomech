using System;
using Anvil.Services;

namespace XM.Shared.Core.Configuration
{
    [ServiceBinding(typeof(XMSettingsService))]
    public class XMSettingsService
    {
        public string SuperAdminCDKey { get; }
        public string RedisIPAddress { get; }
        public string DatabaseSocketPath { get; }
        public bool IsGameServerContext { get; }
        public ServerEnvironmentType ServerEnvironment { get; }

        public string BugWebHookUrl { get; }

        public XMSettingsService()
        {
            SuperAdminCDKey = Environment.GetEnvironmentVariable("XM_SUPER_ADMIN_CD_KEY");
            RedisIPAddress = Environment.GetEnvironmentVariable("NWNX_REDIS_HOST");
            DatabaseSocketPath = Environment.GetEnvironmentVariable("XM_DATABASE_SOCKET_PATH");
            IsGameServerContext = Convert.ToBoolean(Environment.GetEnvironmentVariable("XM_GAME_SERVER_CONTEXT"));
            BugWebHookUrl = Environment.GetEnvironmentVariable("XM_BUG_WEBHOOK_URL");

            var environment = Environment.GetEnvironmentVariable("XM_ENVIRONMENT");
            if (!string.IsNullOrWhiteSpace(environment) &&
                (environment.ToLower() == "prod" || environment.ToLower() == "production"))
            {
                ServerEnvironment = ServerEnvironmentType.Production;
            }
            else if (!string.IsNullOrWhiteSpace(environment) &&
                     (environment.ToLower() == "test" || environment.ToLower() == "testing"))
            {
                ServerEnvironment = ServerEnvironmentType.Test;
            }
            else
            {
                ServerEnvironment = ServerEnvironmentType.Development;
            }
        }

    }
}