using AspNetBase.Domain.Models;

namespace Datiss.Auditing.Domain.Extensions {

    public static class ModelOrderExts
    {

        public static IQueryable<User> SetOrder(this IQueryable<User> query, string orderKey, bool orderDesc = false) {
            orderKey = orderKey.ToLower();
            switch(orderKey) {
                case "username": return orderDesc
                        ? query.OrderByDescending(_=> _.UserName)
                        : query.OrderBy(_ => _.UserName);

                case "firstname": return orderDesc
                        ? query.OrderByDescending(_ => _.FirstName)
                        : query.OrderBy(_ => _.FirstName);

                case "lastname": return orderDesc
                        ? query.OrderByDescending(_ => _.LastName)
                        : query.OrderBy(_ => _.LastName);

                case "nationalcode": return orderDesc
                        ? query.OrderByDescending(_ => _.NationalCode)
                        : query.OrderBy(_ => _.NationalCode);

                case "phonenumber": return orderDesc
                        ? query.OrderByDescending(_ => _.PhoneNumber)
                        : query.OrderBy(_ => _.PhoneNumber);

                case "status": return orderDesc
                        ? query.OrderByDescending(_ => _.Status)
                        : query.OrderBy(_ => _.Status);

                default: return orderDesc
                        ? query.OrderByDescending(_=> _.Id)
                        : query.OrderBy(_ => _.Id);
            }
        }

    }
}
