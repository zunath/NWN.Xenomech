using System;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Json;

namespace XM.Shared.Core.Startup
{
    [ServiceBinding(typeof(KeyNameMappings))]
    public sealed class KeyNameMappings
    {
        public KeyNameMappings(IKeyNameRegistry registry)
        {
            // Centralized registration for name<->code mapping used by JSON converters
            registry.Register(
                "InventorySlotType",
                code => Enum.GetName(typeof(InventorySlotType), code) ?? code.ToString(),
                name => Enum.TryParse<InventorySlotType>(name, true, out var e) ? (int)e : null);

            registry.Register(
                "FeatType",
                code => Enum.GetName(typeof(FeatType), code) ?? code.ToString(),
                name => Enum.TryParse<FeatType>(name, true, out var e) ? (int)e : null);
        }
    }
}


