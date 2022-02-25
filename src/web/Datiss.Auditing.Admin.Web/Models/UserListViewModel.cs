using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Datiss.Auditing.Identity.Models;
using Datiss.Auditing.Web.Common;
using Datiss.Auditing.Core;
using Datiss.Auditing.Enum;
using Datiss.Auditing.Web;
using Datiss.Common.Gaurd;

namespace Datiss.Auditing.Admin.Web.Models
{

    public class UserListFilter {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public int? StatusValue { get; set; }
        public UserStatus Status => StatusValue.HasValue
            ? Enumeration.FromValue<UserStatus>(StatusValue.Value)
            : UserStatus.Unknown;

        public IEnumerable<SelectListItem> StatusSource =>
            SelectListProvider.GetUserStatusItems(StatusValue, true);

        public static UserListFilter FromForm(IFormCollection form) {
            form.CheckArgumentIsNull(nameof(form));

            var result = new UserListFilter
            {
                FullName = form.GetFormString($"filter[{nameof(FullName)}]"),
                NationalCode = form.GetFormString($"filter[{nameof(NationalCode)}]"),
                PhoneNumber = form.GetFormString($"filter[{nameof(PhoneNumber)}]"),
                StatusValue = form.GetFormInt32OrNull($"filter[{nameof(StatusValue)}]"),
                UserName = form.GetFormString($"filter[{nameof(UserName)}]")
            };

            return result;
        }

        private static Dictionary<int, string> _orderMap
            = new Dictionary<int, string>
            {
                { 0, "UserName" },
                { 1, "FirstName" },
                { 2, "LastName" },
                { 3, "NationalCode" },
                { 4, "Status" }
            };

        public string OrderKey(int index)
            => _orderMap[index];

        public string OrderKey(string index)
            => OrderKey(Convert.ToInt32(index));

    }
}
