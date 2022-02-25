using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetBase.Core;
using AspNetBase.Core.Enum;

namespace AspNetBase.Web {

    public static class SelectListProvider {

        public static IList<SelectListItem> AddEmptySelectItem(this IList<SelectListItem> items) {
            items.Insert(0, GetSelectItem());

            return items;
        }

        public static SelectListItem GetSelectItem(bool selected = false)
            => new SelectListItem
            {
                Selected = selected,
                Text = "[انتخاب کنید]",
                Value = ""
            };

        public static IList<SelectListItem> GetUserStatusItems(
            int? selectedId = null,
            bool addEmptySelectItem = false) {

            var result = Enumeration.GetAll<UserStatus>()
                .Select(_ => new SelectListItem
                {
                    Text = _.Title,
                    Value = _.Id.ToString(),
                    Selected = selectedId == _.Id
                }).ToList();

            if (addEmptySelectItem)
                return result.AddEmptySelectItem();

            return result;
        }


    }
}
