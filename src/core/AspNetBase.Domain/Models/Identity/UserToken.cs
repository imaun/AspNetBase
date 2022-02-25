using Microsoft.AspNetCore.Identity;

namespace AspNetBase.Domain.Models {

    public class UserToken : IdentityUserToken<int>, IBaseEntity {

        public UserToken() { }

        public User User { get; set; }
    }
}
