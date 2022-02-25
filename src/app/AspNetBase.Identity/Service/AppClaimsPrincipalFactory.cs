using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using AspNetBase.Domain.Models;
using AspNetBase.Contracts.Identity;
using AspNetBase.Identity.Claims;
using Datiss.Common.Gaurd;

namespace AspNetBase.Identity {

    public class AppClaimsPrincipalFactory: UserClaimsPrincipalFactory<User, Role> {

        private readonly IOptions<IdentityOptions> _options;
        private readonly IAppRoleManager _roleManager;
        private readonly IAppUserManager _userManager;

        public AppClaimsPrincipalFactory(
            IAppUserManager userManager,
            IAppRoleManager roleManager,
            IOptions<IdentityOptions> options)
                : base((UserManager<User>)userManager, (RoleManager<Role>)roleManager, options) {

            userManager.CheckArgumentIsNull(nameof(userManager));
            _userManager = userManager;

            roleManager.CheckArgumentIsNull(nameof(roleManager));
            _roleManager = roleManager;

            options.CheckArgumentIsNull(nameof(options));
            _options = options;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user) {
            var principal = await base.CreateAsync(user); // adds all `Options.ClaimsIdentity.RoleClaimType -> Role Claims` automatically + `Options.ClaimsIdentity.UserIdClaimType -> userId` & `Options.ClaimsIdentity.UserNameClaimType -> userName`
            addCustomClaims(user, principal);
            return principal;
        }

        private static void addCustomClaims(User user, IPrincipal principal) {
            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {

                new Claim(ClaimTypes.NameIdentifier,
                            user.Id.ToString(),
                            ClaimValueTypes.Integer),

                new Claim(ClaimTypes.GivenName,
                            $"{user.FirstName} {user.LastName}" ?? string.Empty),

                new Claim(AspNetBaseClaims.PhoneNumber,
                            user.PhoneNumber),

                new Claim(ClaimTypes.Name,
                            user.UserName),

                new Claim(AspNetBaseClaims.PhoneNumberConfirmed,
                            user.PhoneNumberConfirmed.ToString(),
                            ClaimValueTypes.Boolean)
            });
        }

    }
}
