using Anvil.Services;

namespace NWN.Xenomech.Core
{
    [ServiceBinding(typeof(XMSettingsService))]
    public class XMSettingsService
    {
        public string RedisIPAddress { get; }
        public ServerEnvironmentType ServerEnvironment { get; }

        public XMSettingsService()
        {
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