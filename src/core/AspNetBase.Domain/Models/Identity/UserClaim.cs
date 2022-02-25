using Microsoft.AspNetCore.Identity;

namespace AspNetBase.Domain.Models {

    public class UserClaim : IdentityUserClaim<int>, IBaseEntity {

        public UserClaim() { }


        #region Properties
        public User User { get; set; }

        #endregion
    }
}
