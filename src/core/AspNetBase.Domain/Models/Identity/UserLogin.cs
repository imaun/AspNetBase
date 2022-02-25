using Microsoft.AspNetCore.Identity;

namespace AspNetBase.Domain.Models {

    public class UserLogin : IdentityUserLogin<int> {

        public UserLogin() { }

        #region Properties

        public string IpAddress { get; set; }

        public string UserAgent { get; set; }

        public string Description { get; set; }

        #endregion

        #region Navigations

        public User User { get; set; }

        #endregion
    }
}
