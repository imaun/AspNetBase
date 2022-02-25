using System.Reflection;
using Microsoft.Extensions.Primitives;
using Datiss.Common.Gaurd;

namespace AspNetBase.Web.Common {

    public static class ModelExts {

        public static DataTableInput<T> ToDataTable<T>(this IFormCollection form) where T: class {
            form.CheckArgumentIsNull(nameof(form));

            var result = new DataTableInput<T>(
                draw: form["draw"].FirstOrDefault(),
                columns: form["columns"],
                sortColumn: form["order[0][column]"].FirstOrDefault(),
                sortColumnDir: form["order[0][dir]"].FirstOrDefault(),
                length: form["length"].FirstOrDefault(),
                start: form["start"].FirstOrDefault(),
                searchValue: form["search[value]"]);

            return result;
        }

        public static int? ConvertToInt32OrNull(this string? value) {
            if (value.IsNullOrEmpty())
                return null;

            return Convert.ToInt32(value);
        }

        public static long? ConvertToInt64OrNull(this string? value) {
            if (value.IsNullOrEmpty())
                return null;

            return Convert.ToInt64(value);
        }

        public static object GetPropertyValue<T>(this T _type, string propertyName) where T: class {
            Type tType = _type.GetType();
            PropertyInfo[] properties = tType.GetProperties();
            var property = properties.FirstOrDefault(_ => _.Name == propertyName);
            if(property != null) {
                return property.GetValue(_type);
            }

            return null;
        }

        public static bool ExistInForm(this IEnumerable<KeyValuePair<string, StringValues>> values, string key)
            => values.Any(_ => string.Equals(_.Key, key, StringComparison.InvariantCultureIgnoreCase));

        public static string GetFormValue(this IEnumerable<KeyValuePair<string, StringValues>> values, string key) {
            if(!ExistInForm(values, key))
                return null;

            var item = values.FirstOrDefault(
                _ => string.Equals(_.Key, key, StringComparison.InvariantCultureIgnoreCase)
                );
            return item.Value;
        }

        public static string? GetFormString(this IEnumerable<KeyValuePair<string, StringValues>> values, string key) {
            if (!ExistInForm(values, key)) 
                return null;

            return values.FirstOrDefault(
                _ => string.Equals(_.Key, key, StringComparison.InvariantCultureIgnoreCase)
                        ).Value;
        }

        public static int? GetFormInt32OrNull(this IEnumerable<KeyValuePair<string, StringValues>> values, string key) 
            => int.TryParse(GetFormValue(values, key), out var val) ? val : null;

        public static long? GetFormInt64OrNull(this IEnumerable<KeyValuePair<string, StringValues>> values, string key)
            => long.TryParse(GetFormValue(values, key), out var val) ? val : null;

        public static int GetFormInt32(this IEnumerable<KeyValuePair<string, StringValues>> values, string key)
            => int.TryParse(GetFormValue(values, key), out var val) ? val : default(int);

        public static long GetFormInt64(this IEnumerable<KeyValuePair<string, StringValues>> values, string key)
            => long.TryParse(GetFormValue(values, key), out var val) ? val : default(long);

    }
}
