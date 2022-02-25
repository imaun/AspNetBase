using Microsoft.AspNetCore.Identity;

namespace AspNetBase.Domain.Models
{

    public class RoleClaim : IdentityRoleClaim<int>, IBaseEntity
    {
        public RoleClaim() { }

        #region Navigations
        public Role Role { get; set; }

        #endregion
    }
}
