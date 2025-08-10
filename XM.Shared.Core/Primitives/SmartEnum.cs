using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace XM.Shared.Core.Primitives
{
    public abstract class SmartEnum<TEnum> where TEnum : SmartEnum<TEnum>
    {
        public string Name { get; }
        public int Value { get; }

        protected SmartEnum(string name, int value)
        {
            Name = name;
            Value = value;
        }

        private struct Cache
        {
            public IReadOnlyList<TEnum> Items { get; init; }
            public Dictionary<int, TEnum> ByValue { get; init; }
            public Dictionary<string, TEnum> ByName { get; init; }
        }

        private static readonly Lazy<Cache> _cache = new Lazy<Cache>(BuildCache, System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        private static Cache BuildCache()
        {
            var fields = typeof(TEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(f => f.FieldType == typeof(TEnum));

            var instances = new List<TEnum>();
            foreach (var field in fields)
            {
                var value = (TEnum?)field.GetValue(null); // Triggers type initialization of TEnum
                if (value != null)
                {
                    instances.Add(value);
                }
            }

            var items = instances.OrderBy(i => i.Value).ToList().AsReadOnly();
            return new Cache
            {
                Items = items,
                ByValue = items.ToDictionary(i => i.Value),
                ByName = items.ToDictionary(i => i.Name, StringComparer.Ordinal)
            };
        }

        public static IReadOnlyCollection<TEnum> List => _cache.Value.Items;

        public static TEnum FromValue(int value)
        {
            var byValue = _cache.Value.ByValue;
            if (byValue.TryGetValue(value, out var result))
                return result;
            throw new ArgumentOutOfRangeException(nameof(value), $"No {typeof(TEnum).Name} with value {value} was found.");
        }

        public static TEnum? TryFromValue(int value)
        {
            _cache.Value.ByValue.TryGetValue(value, out var result);
            return result;
        }

        public static TEnum FromName(string name, bool ignoreCase = false)
        {
            var cache = _cache.Value;
            if (ignoreCase)
            {
                var match = cache.Items.FirstOrDefault(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));
                if (match != null)
                    return match;
            }
            else if (cache.ByName.TryGetValue(name, out var result))
            {
                return result;
            }

            throw new ArgumentOutOfRangeException(nameof(name), $"No {typeof(TEnum).Name} with name '{name}' was found.");
        }

        public override string ToString() => Name;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not SmartEnum<TEnum> other) return false;
            return Value == other.Value && GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(typeof(TEnum), Value);
        }

        public static bool operator ==(SmartEnum<TEnum>? left, SmartEnum<TEnum>? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(SmartEnum<TEnum>? left, SmartEnum<TEnum>? right)
        {
            return !(left == right);
        }
    }
}


