using System;
using System.Reflection;
using System.Text.Json;
using Anvil.Services;

namespace XM.Shared.Core.Json
{
    [ServiceBinding(typeof(JsonCleanupService))]
    [ServiceBinding(typeof(IDisposable))]
    public class JsonCleanupService: IDisposable
    {
        // This method is a hack to work around the fact that System.Text.Json does not properly clean itself up on an unload.
        // Source: https://github.com/dotnet/runtime/blob/dae890906431049d32e24d498a1d707a441a64a8/src/libraries/System.Text.Json/tests/System.Text.Json.Tests/Serialization/CacheTests.cs#L213
        // Related conversations:
        //      https://github.com/dotnet/runtime/issues/65323
        //      https://github.com/dotnet/runtime/issues/13283
        private void ClearJsonCache()
        {
            var stjAssembly = typeof(JsonSerializer).Assembly;

            var updateHandlerType = stjAssembly.GetType("System.Text.Json.JsonSerializerOptionsUpdateHandler", throwOnError: false);
            if (updateHandlerType == null)
            {
                throw new InvalidOperationException("Could not find System.Text.Json.JsonSerializerOptionsUpdateHandler in the assembly.");
            }

            var clearCacheMethod = updateHandlerType.GetMethod("ClearCache",
                BindingFlags.Public | BindingFlags.Static);

            if (clearCacheMethod == null)
            {
                throw new InvalidOperationException("Could not find ClearCache method on JsonSerializerOptionsUpdateHandler.");
            }

            clearCacheMethod.Invoke(null, [null]);
        }

        public void Dispose()
        {
            ClearJsonCache();
        }
    }
}
