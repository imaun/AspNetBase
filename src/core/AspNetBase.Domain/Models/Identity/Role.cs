using Microsoft.AspNetCore.Identity;

namespace AspNetBase.Domain.Models
{

    public class Role : IdentityRole<int>, IBaseEntity
    {

        public Role() :base() {
            Users = new HashSet<UserRole>();
            Claims = new HashSet<RoleClaim>();
        }

        #region Properties

        public string Title { get; set; }

        public string Description { get; set; }

        #endregion

        #region Navigations

        public HashSet<UserRole> Users { get; set; }

        public HashSet<RoleClaim> Claims { get; set; }

        #endregion
    }
}
