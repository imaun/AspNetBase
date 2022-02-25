using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Datiss.Common.Identity;
using AspNetBase.Persistence;
using AspNetBase.Domain.Models;
using AspNetBase.Contracts.Identity;
using AspNetBase.Contracts.Persistence;

namespace AspNetBase.Identity {

    public class AppUserStore 
        : UserStore<User, Role, AppDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
        , IAppUserStore {

        private readonly IAppDbContext _db;
        private readonly IdentityErrorDescriber _describer;

        public AppUserStore(
            IAppDbContext db,
            IdentityErrorDescriber describer
            ) : base((AppDbContext)db, describer) {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _describer = describer ?? throw new ArgumentNullException(nameof(describer));
        }

        #region BaseClass

        protected override UserClaim CreateUserClaim(User user, Claim claim) {
            var userClaim = new UserClaim { UserId = user.Id };
            userClaim.InitializeFromClaim(claim);
            return userClaim;
        }

        protected override UserLogin CreateUserLogin(User user, UserLoginInfo login) {
            return new UserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            };
        }

        protected override UserRole CreateUserRole(User user, Role role) {
            return new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }

        protected override UserToken CreateUserToken(User user, string loginProvider, string name, string value) {
            return new UserToken
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };
        }

        Task IAppUserStore.AddUserTokenAsync(UserToken token)
            => base.AddUserTokenAsync(token);

        Task<Role> IAppUserStore.FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
            => base.FindRoleAsync(normalizedRoleName, cancellationToken);

        Task<UserToken> IAppUserStore.FindTokenAsync(User user, string loginProvider, string name, CancellationToken cancellationToken)
            => base.FindTokenAsync(user, loginProvider, name, cancellationToken);

        Task<User> IAppUserStore.FindUserAsync(int userId, CancellationToken cancellationToken)
            => base.FindUserAsync(userId, cancellationToken);

        Task<UserLogin> IAppUserStore.FindUserLoginAsync(int userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
            => base.FindUserLoginAsync(userId, loginProvider, providerKey, cancellationToken);

        Task<UserLogin> IAppUserStore.FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
            => base.FindUserLoginAsync(loginProvider, providerKey, cancellationToken);

        Task<UserRole> IAppUserStore.FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
            => base.FindUserRoleAsync(userId, roleId, cancellationToken);

        Task IAppUserStore.RemoveUserTokenAsync(UserToken token)
            => base.RemoveUserTokenAsync(token);

        #endregion

        #region CustomMethods

        // Add custom methods here

        #endregion
    }
}
