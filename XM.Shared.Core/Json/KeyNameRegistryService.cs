using System;
using System.Collections.Concurrent;
using Anvil.Services;
using System.Linq;
using System.Reflection;

namespace XM.Shared.Core.Json
{
    [ServiceBinding(typeof(IKeyNameRegistry))]
    [ServiceBinding(typeof(IInitializable))]
    public class KeyNameRegistryService : IKeyNameRegistry, IInitializable
    {
        private readonly ConcurrentDictionary<string, Func<int, string>> _intToName = new();
        private readonly ConcurrentDictionary<string, Func<string, int?>> _nameToInt = new();
        private readonly ConcurrentDictionary<string, System.Collections.Generic.IReadOnlyDictionary<int, string>> _codeToNameCache = new();

        public KeyNameRegistryService() { }

        public void Init()
        {
            // Make it available to the static facade
            KeyNameRegistry.SetProvider(this);
            // Scan and auto-register annotated types for already-loaded assemblies
            AutoRegisterFromLoadedAssemblies();
            // Also register for future assembly loads
            AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        }

        private void OnAssemblyLoad(object? sender, AssemblyLoadEventArgs args)
        {
            try
            {
                RegisterTypesFromAssembly(args.LoadedAssembly);
            }
            catch
            {
                // swallow to avoid startup issues; converters will fall back to numeric names
            }
        }

        private void AutoRegisterFromLoadedAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in assemblies)
            {
                RegisterTypesFromAssembly(asm);
            }
        }

        private void RegisterTypesFromAssembly(Assembly asm)
        {
            Type[] types;
            try { types = asm.GetTypes(); }
            catch (ReflectionTypeLoadException ex) { types = ex.Types.Where(t => t != null).ToArray()!; }

            foreach (var type in types)
            {
                if (type == null) continue;
                var attr = type.GetCustomAttribute<KeyNameDomainAttribute>();
                if (attr == null) continue;

                var domain = attr.Domain;

                if (type.IsEnum)
                {
                    Register(domain,
                        code => Enum.GetName(type, code) ?? code.ToString(),
                        name => Enum.TryParse(type, name, true, out var e) ? Convert.ToInt32(e) : (int?)null);
                    continue;
                }

                // SmartEnum-style: class with public static fields of its own type and int Value / string Name
                if (type.IsClass)
                {
                    var valueProp = type.GetProperty("Value", BindingFlags.Public | BindingFlags.Instance);
                    var nameProp = type.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
                    if (valueProp == null || nameProp == null) continue;

                    var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                        .Where(f => f.FieldType == type);

                    var instances = fields
                        .Select(f => f.GetValue(null))
                        .Where(o => o != null)
                        .ToList();

                    var codeToName = instances.ToDictionary(
                        o => (int)(valueProp.GetValue(o) ?? 0),
                        o => (string)(nameProp.GetValue(o) ?? string.Empty));

                    var nameToCode = codeToName.ToDictionary(kv => kv.Value, kv => (int?)kv.Key, StringComparer.OrdinalIgnoreCase);

                    Register(domain,
                        code => codeToName.TryGetValue(code, out var n) ? n : code.ToString(),
                        name => nameToCode.TryGetValue(name, out var v) ? v : null);
                }
            }
        }

        public void Register(string domain, Func<int, string> toName, Func<string, int?> toInt)
        {
            _intToName[domain] = toName;
            _nameToInt[domain] = toInt;
            // attempt to precompute a small snapshot map for enumeration
            try
            {
                var map = new System.Collections.Generic.Dictionary<int, string>();
                // Try a reasonable code range 0..1024; names for unmapped return numeric strings
                for (var i = 0; i <= 1024; i++)
                {
                    var name = toName(i);
                    if (!string.IsNullOrEmpty(name) && name != i.ToString())
                    {
                        map[i] = name;
                    }
                }
                _codeToNameCache[domain] = map;
            }
            catch
            {
                // ignore precompute failures
            }
        }

        public string ToName(string domain, int code)
        {
            if (_intToName.TryGetValue(domain, out var fn))
                return fn(code);
            return code.ToString();
        }

        public int? ToCode(string domain, string name)
        {
            if (_nameToInt.TryGetValue(domain, out var fn))
                return fn(name);
            return null;
        }

        public System.Collections.Generic.IReadOnlyDictionary<int, string>? GetCodeToNameMap(string domain)
        {
            if (_codeToNameCache.TryGetValue(domain, out var map)) return map;
            return null;
        }
    }
}


