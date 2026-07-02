namespace Console.EnumAlternative.Features
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public abstract class EnumerationEnum<T> : IEquatable<T>, IComparable<T> where T : EnumerationEnum<T>
    {
        private static readonly Lazy<Dictionary<int, T>> _byValue = new(CreateByValue, LazyThreadSafetyMode.ExecutionAndPublication);

        private static readonly Lazy<Dictionary<string, T>> _byName = new(CreateByName, LazyThreadSafetyMode.ExecutionAndPublication);

        protected EnumerationEnum(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public int Value { get; }

        public string Name { get; }

        public static int Count
        {
            get
            {
                return _byValue.Value.Values.ToList().Count;
            }
        }

        private static Dictionary<int, T> CreateByValue()
        {
            var values = LoadValues();

            return values.ToDictionary(x => x.Value);
        }

        private static Dictionary<string, T> CreateByName()
        {
            var values = LoadValues();

            return values.ToDictionary(x => x.Name);
        }

        private static List<T> LoadValues()
        {
            // Erzwingt die Initialisierung der statischen Felder
            RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);

            var fields = typeof(T)
                .GetFields(BindingFlags.Public |
                           BindingFlags.Static |
                           BindingFlags.DeclaredOnly)
                .Where(f => f.FieldType == typeof(T));

            var list = new List<T>();

            foreach (var field in fields)
            {
                if (field.GetValue(null) is T item)
                {
                    list.Add(item);
                }
            }

            return list;
        }

        public static IReadOnlyCollection<T> GetValues() => _byValue.Value.Values.ToList().AsReadOnly();

        public static T FromValue(int value) => _byValue.Value[value];

        public static T FromName(string name) => _byName.Value[name];

        public static bool TryFromValue(int value, out T result) => _byValue.Value.TryGetValue(value, out result!);

        public static bool TryFromName(string name, out T result) => _byName.Value.TryGetValue(name, out result!);

        public override string ToString() => Name;

        public override int GetHashCode() => Value;

        public override bool Equals(object obj)  => obj is T other && Equals(other);

        public bool Equals(T other) => other is not null && Value == other.Value;

        public int CompareTo(T other) => Value.CompareTo(other?.Value ?? -1);

        public static bool operator ==(EnumerationEnum<T> left, EnumerationEnum<T> right) => Equals(left, right);

        public static bool operator !=(EnumerationEnum<T> left, EnumerationEnum<T> right) => !Equals(left, right);

        public static implicit operator int(EnumerationEnum<T> value) => value.Value;

        public static implicit operator string(EnumerationEnum<T> value) => value.Name;
    }
}
