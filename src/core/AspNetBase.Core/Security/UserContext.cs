using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using AspNetBase.Identity.Claims;

namespace AspNetBase.Core
{

    public interface IUserContext {
        bool IsAuthenticated { get; }
        int UserId { get; }
        string DisplayName { get; }
        string PhoneNumber { get; }
        string UserName { get; }

    }


    public class UserContext : IUserContext {

        public UserContext(IHttpContextAccessor httpContextAccessor) {
            httpContextAccessor.CheckArgumentIsNull(nameof(httpContextAccessor));
            Principal = httpContextAccessor.HttpContext.User;
            IsAuthenticated = Principal?.Identity.IsAuthenticated ?? false;
            loadData();
        }

        public bool IsAuthenticated { get; }

        public int UserId { get; protected set; }

        public string DisplayName { get; protected set; }

        public string PhoneNumber { get; protected set; }

        public string UserName { get; protected set; }

        protected ClaimsPrincipal Principal { get; }

        private void loadData() {
            if (!IsAuthenticated) 
                return;

            UserId = Principal.Identity.GetUserId();
            UserName = Principal.Identity.GetUserName();
            DisplayName = Principal.Identity.GetUserDisplayName();
            PhoneNumber = Principal.Identity.GetUserClaimValue(AspNetBaseClaims.PhoneNumber);
        }

    }

}
