using System;

namespace XM.Shared.Core.Json
{
    public interface IKeyNameRegistry
    {
        void Register(string domain, Func<int, string> toName, Func<string, int?> toInt);
        string ToName(string domain, int code);
        int? ToCode(string domain, string name);
        System.Collections.Generic.IReadOnlyDictionary<int, string>? GetCodeToNameMap(string domain);
    }

    // Thin facade used by JSON converters; delegates to runtime service provider
    public static class KeyNameRegistry
    {
        private static IKeyNameRegistry? _provider;
        private static readonly object _lock = new();
        private static readonly System.Collections.Generic.List<(string domain, Func<int, string> toName, Func<string, int?> toInt)> _pending = new();

        internal static void SetProvider(IKeyNameRegistry provider)
        {
            lock (_lock)
            {
                _provider = provider;
                // Flush pending
                foreach (var (domain, toName, toInt) in _pending)
                {
                    _provider.Register(domain, toName, toInt);
                }
                _pending.Clear();
            }
        }

        public static void Register(string domain, Func<int, string> toName, Func<string, int?> toInt)
        {
            lock (_lock)
            {
                if (_provider is null)
                {
                    _pending.Add((domain, toName, toInt));
                }
                else
                {
                    _provider.Register(domain, toName, toInt);
                }
            }
        }

        public static string ToName(string domain, int code)
        {
            return _provider?.ToName(domain, code) ?? code.ToString();
        }

        public static int? ToCode(string domain, string name)
        {
            return _provider?.ToCode(domain, name);
        }

        public static System.Collections.Generic.IReadOnlyDictionary<int, string>? GetCodeToNameMap(string domain)
        {
            return _provider?.GetCodeToNameMap(domain);
        }
    }
}


