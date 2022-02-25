namespace AspNetBase.Identity.Models { 

    public class UserLoginResult {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public bool RequiresTwoFactor { get; set; }
        public string TwoFactorToken { get; set; }
    }

}
