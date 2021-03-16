using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Photo.Manufacture.SharedKernel
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1210:\"Equals\" and the comparison operators should be overridden when implementing \"IComparable\"", Justification = "Not needed in this semanitcs")]
    public abstract class BaseEnumeration : IComparable
    {
        protected BaseEnumeration(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Name { get; private set; }

        public int Id { get; private set; }

        public static bool operator ==(BaseEnumeration left, BaseEnumeration right) => Equals(left, null) ? Equals(right, null) : left.Equals(right);

        public static bool operator !=(BaseEnumeration left, BaseEnumeration right) => !(left == right);

        public static IEnumerable<T> GetAll<T>()
            where T : BaseEnumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static T FromValue<T>(int value)
            where T : BaseEnumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static int AbsoluteDifference(BaseEnumeration firstValue, BaseEnumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromDisplayName<T>(string displayName)
            where T : BaseEnumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        public override string ToString() => this.Name;

        public override bool Equals(object obj)
        {
            var otherValue = obj as BaseEnumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = this.GetType().Equals(obj.GetType());
            var valueMatches = this.Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => this.Id.GetHashCode();

        public int CompareTo(object obj) => this.Id.CompareTo(((BaseEnumeration)obj).Id);

        private static T Parse<T, TK>(TK value, string description, Func<T, bool> predicate)
            where T : BaseEnumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }
    }
}
