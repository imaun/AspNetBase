using Microsoft.AspNetCore.Identity;

namespace AspNetBase.Core.Configuration
{
    public class AppIdentityOptions
    {
        public string DefaultUserPassword { get; set; }
        public bool EnableTwoFactorAuthentication { get; set; }
        public PasswordOptions PasswordOptions { get; set; }
        public CookieOptions CookieOptions { get; set; }
        public LockoutOptions LockoutOptions { get; set; }
        public bool EnableEmailConfirmation { get; set; }
        public TimeSpan EmailConfirmationTokenProviderLifespan { get; set; }
        public int ChangePasswordReminderDays { get; set; }
        public string[] EmailsBanList { get; set; }
        public string[] PasswordsBanList { get; set; }
    }

}
