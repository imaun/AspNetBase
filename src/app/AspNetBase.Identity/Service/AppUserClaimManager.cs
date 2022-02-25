using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using AspNetBase.Domain.Models;
using AspNetBase.Contracts.Identity;

namespace AspNetBase.Identity {

    public class AppUserClaimManager : IAppUserClaimManager {

        private readonly IAppUserManager _userManager;
        private readonly IAppSignInManager _signInManager;

        public AppUserClaimManager(
            IAppUserManager userManager,
            IAppSignInManager signInManager) {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task ReplaceClaimAsync(User user, string claimType, string claimValue) {
            var claims = await _userManager.GetClaimsAsync(user);
            var claim = claims.FirstOrDefault(_ => _.Type == claimType);
            var newClaim = new Claim(claimType, claimValue);

            IdentityResult result = null;
            if (claim != null) {
                result = await _userManager.ReplaceClaimAsync(user, claim, newClaim);
            }
            else {
                result = await _userManager.AddClaimAsync(user, newClaim);
            }

            await _signInManager.RefreshSignInAsync(user);
        }

    }
}
