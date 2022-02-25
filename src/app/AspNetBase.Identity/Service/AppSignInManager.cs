using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using AspNetBase.Domain.Models;
using AspNetBase.Contracts.Identity;

namespace AspNetBase.Identity {

    public class AppSignInManager : SignInManager<User>, IAppSignInManager {

        private readonly IAppUserManager _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        private readonly IOptions<IdentityOptions> _optionsAccessor;
        private readonly ILogger<AppSignInManager> _logger;
        private readonly IAuthenticationSchemeProvider _schemes;

        public AppSignInManager(
            IAppUserManager userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<AppSignInManager> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation)
                : base((UserManager<User>)userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation) {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(_contextAccessor));
            _claimsFactory = claimsFactory ?? throw new ArgumentNullException(nameof(_claimsFactory));
            _optionsAccessor = optionsAccessor ?? throw new ArgumentNullException(nameof(_optionsAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
            _schemes = schemes ?? throw new ArgumentNullException(nameof(_schemes));
        }

        #region BaseClass

        Task<bool> IAppSignInManager.IsLockedOut(User user)
            => base.IsLockedOut(user);

        Task<SignInResult> IAppSignInManager.LockedOut(User user)
            => base.LockedOut(user);

        Task<SignInResult> IAppSignInManager.PreSignInCheck(User user)
            => base.PreSignInCheck(user);


        Task IAppSignInManager.ResetLockout(User user)
            => base.ResetLockout(user);

        Task<SignInResult> IAppSignInManager.SignInOrTwoFactorAsync(User user, bool isPersistent, string loginProvider, bool bypassTwoFactor)
            => base.SignInOrTwoFactorAsync(user, isPersistent, loginProvider, bypassTwoFactor);


        #endregion

        #region CustomMethods

        public bool IsCurrentUserSignedIn()
            => IsSignedIn(_contextAccessor.HttpContext.User);

        public Task<User> ValidateCurrentUserSecurityStampAsync()
            => ValidateSecurityStampAsync(_contextAccessor.HttpContext.User);

        #endregion
    }
}
