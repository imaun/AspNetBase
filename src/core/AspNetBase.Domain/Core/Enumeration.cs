using System.Reflection;

namespace AspNetBase.Core
{
    /// <summary>
    /// Base class for Enum types.
    /// This implements safe enum pattern.
    /// More info : https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
    /// and implementions from : https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/Services/Ordering/Ordering.Domain/SeedWork/Enumeration.cs
    /// </summary>
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        public string Title { get; private set; }

        protected Enumeration(int id, string name, string title = "") 
            => (Id, Name, Title) = (id, name, title);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetProperties(BindingFlags.Public |
                                    BindingFlags.Static |
                                    BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();

        public override bool Equals(object obj) {
            if (obj is not Enumeration otherValue) {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public override int GetHashCode() => Id.GetHashCode();

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue) {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

    }
}
