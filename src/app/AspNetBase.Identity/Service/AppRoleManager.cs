using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AspNetBase.Domain.Models;
using AspNetBase.Contracts.Identity;
using AspNetBase.Contracts.Persistence;
using Datiss.Common.Gaurd;
using AspNetBase.Persistence;
using Datiss.Common.Identity;

namespace AspNetBase.Identity
{
    public class AppRoleManager : RoleManager<Role>, IAppRoleManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAppDbContext _db;
        private readonly IdentityErrorDescriber _errors;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<AppRoleManager> _logger;
        private readonly IEnumerable<IRoleValidator<Role>> _roleValidators;
        private readonly IAppRoleStore _store;
        private readonly DbSet<User> _users;

        public AppRoleManager(
            IAppRoleStore store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<AppRoleManager> logger,
            IHttpContextAccessor contextAccessor,
            IAppDbContext db) : base((RoleStore<Role, AuditingDbContext, int, UserRole, RoleClaim>)store, roleValidators, keyNormalizer, errors, logger) {

            store.CheckArgumentIsNull(nameof(store));
            _store = store;

            roleValidators.CheckArgumentIsNull(nameof(roleValidators));
            _roleValidators = roleValidators;

            keyNormalizer.CheckArgumentIsNull(nameof(keyNormalizer));
            _keyNormalizer = keyNormalizer;

            errors.CheckArgumentIsNull(nameof(errors));
            _errors = errors;

            logger.CheckArgumentIsNull(nameof(logger));
            _logger = logger;

            contextAccessor.CheckArgumentIsNull(nameof(contextAccessor));
            _contextAccessor = contextAccessor;

            db.CheckArgumentIsNull(nameof(db));
            _db = db;

            _users = _db.Set<User>();
        }

        private long _currentUserId => _contextAccessor.HttpContext.User.Identity.GetUserId();

        public async Task<IdentityResult> AddOrUpdateRoleClaimsAsync(
            long roleId,
            string roleClaimType,
            IList<string> selectedRoleClaimValues) {

            var role = await FindRoleIncludeRoleClaimsAsync(roleId);
            if (role == null) {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotFound",
                    Description = "نقش مورد نظر یافت نشد."
                });
            }

            var currentRoleClaimValues = role.Claims.Where(roleClaim => roleClaim.ClaimType == roleClaimType)
                                                    .Select(roleClaim => roleClaim.ClaimValue)
                                                    .ToList();

            selectedRoleClaimValues ??= new List<string>();
            var newClaimValuesToAdd = selectedRoleClaimValues.Except(currentRoleClaimValues).ToList();
            foreach (var claimValue in newClaimValuesToAdd) {
                role.Claims.Add(new RoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = roleClaimType,
                    ClaimValue = claimValue
                });
            }

            var removedClaimValues = currentRoleClaimValues.Except(selectedRoleClaimValues).ToList();
            foreach (var claimValue in removedClaimValues) {
                var roleClaim = role.Claims.SingleOrDefault(rc => rc.ClaimValue == claimValue &&
                                                                  rc.ClaimType == roleClaimType);
                if (roleClaim != null) {
                    role.Claims.Remove(roleClaim);
                }
            }

            return await UpdateAsync(role);
        }

        public async Task<Role> FindRoleIncludeRoleClaimsAsync(long roleId)
            => await Roles.Include(_ => _.Claims).FirstOrDefaultAsync(_ => _.Id == roleId);

        public IList<Role> FindUserRoles(long userId) {
            var query = from role in Roles
                        from user in role.Users
                        where user.UserId == userId
                        select role;

            return query.OrderBy(_ => _.Name).ToList();
        }

        public async Task<List<Role>> GetAllCustomRolesAsync() => await Roles.ToListAsync();

        public IList<Role> GetRolesForCurrentUser() => GetRolesForUser(_currentUserId);

        public IList<Role> GetRolesForUser(long userId) {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
                return new List<Role>();

            return roles.ToList();
        }

        public IList<UserRole> GetUserRolesInRole(string roleName)
            => Roles.Where(_ => _.Name == roleName)
                    .SelectMany(_ => _.Users)
                    .ToList();

        public IList<User> GetUsersInRole(string roleName)
            => _users.Where(_ => (from role in Roles
                                  where role.Name == roleName
                                  from user in role.Users
                                  select user.UserId)
                            .Contains(_.Id))
                            .ToList();

        public bool IsCurrentUserInRole(string roleName)
            => IsUserInRole(_currentUserId, roleName);

        public bool IsUserInRole(long userId, string roleName)
            => (from role in Roles
                where role.Name == roleName
                from user in role.Users
                where user.UserId == userId
                select role).FirstOrDefault() != null;

    }
}
