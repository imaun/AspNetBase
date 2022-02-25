using Microsoft.AspNetCore.Identity;

namespace AspNetBase.Identity {

    public class TwoFactorTokenSmsProvider<TUser> 
        : PhoneNumberTokenProvider<TUser> where TUser : class
    {

        public TwoFactorTokenSmsProvider() { }
    }

    public class TwoFactorTokenSmsProviderOptions : DataProtectionTokenProviderOptions
    {

        public TwoFactorTokenSmsProviderOptions() {
            Name = "SmsTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(2);
        }

    }

}
