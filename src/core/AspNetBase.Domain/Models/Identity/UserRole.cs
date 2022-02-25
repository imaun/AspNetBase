using Microsoft.AspNetCore.Identity;

namespace AspNetBase.Domain.Models {

    public class UserRole : IdentityUserRole<int>, IBaseEntity {

        public UserRole() { }

        #region Navigations

        public User User { get; set; }

        public Role Role { get; set; }

        #endregion

    }
}
