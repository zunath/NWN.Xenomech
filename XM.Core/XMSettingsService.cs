using System;
using Anvil.Services;

namespace XM.Core
{
    [ServiceBinding(typeof(XMSettingsService))]
    public class XMSettingsService
    {
        public string SuperAdminCDKey { get; }
        public string RedisIPAddress { get; }
        public ServerEnvironmentType ServerEnvironment { get; }

        public XMSettingsService()
        {
            SuperAdminCDKey = Environment.GetEnvironmentVariable("XM_SUPER_ADMIN_CD_KEY");
            RedisIPAddress = Environment.GetEnvironmentVariable("NWNX_REDIS_HOST");

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