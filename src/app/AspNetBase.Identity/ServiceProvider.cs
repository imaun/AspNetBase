using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using AspNetBase.Contracts.Identity;
using AspNetBase.Domain.Models;
using AspNetBase.Core.Configuration;
using AspNetBase.Identity;
using AspNetBase.Persistence;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceProvider {

        public static void AddAuditingIdentity(this IServiceCollection services, AppConfig config) {
            if (config == null) {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddIdentity<User, Role>(_ => {
                setPasswordOptions(_.Password, config);
                setSignInOptions(_.SignIn, config);
                setUserOptions(_.User);
                setLockoutOptions(_.Lockout, config);
            }).AddUserStore<AppUserStore>()
                .AddUserManager<AppUserManager>()
                .AddRoleStore<AppRoleStore>()
                .AddSignInManager<AppSignInManager>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<TwoFactorTokenSmsProvider<User>>("sms")
                .AddEntityFrameworkStores<AppDbContext>()
                .AddClaimsPrincipalFactory<AppClaimsPrincipalFactory>();

            services.ConfigureApplicationCookie(options => {
                setApplicationCookieOptions(options, config);
            });

            services.addConfirmEmailDataProtectorTokenOptions(config);

            services.AddScoped<IAppRoleManager, AppRoleManager>();
            services.AddScoped<IAppRoleStore, AppRoleStore>();
            services.AddScoped<IAppSignInManager, AppSignInManager>();
            services.AddScoped<IAppUserManager, AppUserManager>();
            services.AddScoped<IAppUserStore, AppUserStore>();
            services.AddScoped<IAppUserClaimManager, AppUserClaimManager>();
            services.AddScoped<IPasswordValidator<User>, CustomPasswordValidator>();

        }

        private static void addConfirmEmailDataProtectorTokenOptions(
            this IServiceCollection services, AppConfig config
        ) {
            services.Configure<IdentityOptions>(options => {
                options.Tokens.EmailConfirmationTokenProvider = "aspnetbaseconfirmtoken";
            });

            services.Configure<ConfirmEmailDataProtectionTokenProviderOptions>(options => {
                options.TokenLifespan = config.Identity.EmailConfirmationTokenProviderLifespan;
            });
        }

        private static void enableImmediateLogout(this IServiceCollection services) {
            services.Configure<SecurityStampValidatorOptions>(options => {
                // enables immediate logout, after updating the user's stat.
                options.ValidationInterval = TimeSpan.Zero;
                options.OnRefreshingPrincipal = principalContext => {
                    // Invoked when the default security stamp validator replaces the user's ClaimsPrincipal in the cookie.

                    //var newId = new ClaimsIdentity();
                    //newId.AddClaim(new Claim("PreviousName", principalContext.CurrentPrincipal.Identity.Name));
                    //principalContext.NewPrincipal.AddIdentity(newId);

                    return Task.CompletedTask;
                };
            });
        }

        private static void setApplicationCookieOptions(
            //IServiceProvider provider,
            CookieAuthenticationOptions identityOptionsCookies,
            AppConfig config
        ) {
            var options = config.Identity.CookieOptions;
            identityOptionsCookies.Cookie.Name = options.CookieName;
            identityOptionsCookies.Cookie.HttpOnly = true;
            identityOptionsCookies.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            identityOptionsCookies.Cookie.SameSite = SameSiteMode.Lax;
            identityOptionsCookies.Cookie.IsEssential = true; //  this cookie will always be stored regardless of the user's consent

            identityOptionsCookies.ExpireTimeSpan = options.ExpireTimeSpan;
            identityOptionsCookies.SlidingExpiration = options.SlidingExpiration;
            identityOptionsCookies.LoginPath = options.LoginPath;
            identityOptionsCookies.LogoutPath = options.LogoutPath;
            identityOptionsCookies.AccessDeniedPath = options.AccessDeniedPath;

            //if (options.UseDistributedCacheTicketStore) {
            //    // To manage large identity cookies
            //    identityOptionsCookies.SessionStore = provider.GetRequiredService<ITicketStore>();
            //}
        }

        private static void setLockoutOptions(
            LockoutOptions identityOptionsLockout,
            AppConfig config) {
            var options = config.Identity.LockoutOptions;
            identityOptionsLockout.AllowedForNewUsers = options.AllowedForNewUsers;
            identityOptionsLockout.DefaultLockoutTimeSpan = options.DefaultLockoutTimeSpan;
            identityOptionsLockout.MaxFailedAccessAttempts = options.MaxFailedAccessAttempts;
        }

        private static void setPasswordOptions(
            PasswordOptions identityOptionsPassword,
            AppConfig config) {
            var options = config.Identity.PasswordOptions;
            identityOptionsPassword.RequireDigit = options.RequireDigit;
            identityOptionsPassword.RequireLowercase = options.RequireLowercase;
            identityOptionsPassword.RequireNonAlphanumeric = options.RequireNonAlphanumeric;
            identityOptionsPassword.RequireUppercase = options.RequireUppercase;
            identityOptionsPassword.RequiredLength = options.RequiredLength;
        }

        private static void setSignInOptions(
            SignInOptions identityOptionsSignIn,
            AppConfig config
        ) => identityOptionsSignIn.RequireConfirmedEmail = config.Identity.EnableEmailConfirmation;

        private static void setUserOptions(UserOptions identityOptionsUser)
            => identityOptionsUser.RequireUniqueEmail = false;
    }
}
