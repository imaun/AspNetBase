using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AspNetBase.Domain.Models;
using AspNetBase.Persistence;
using AspNetBase.Contracts.Identity;
using AspNetBase.Contracts.Persistence;

namespace AspNetBase.Identity {

    public class AppRoleStore : RoleStore<Role, AppDbContext, int, UserRole, RoleClaim>, IAppRoleStore {

        private readonly IAppDbContext _db;
        private readonly IdentityErrorDescriber _describer;

        public AppRoleStore(
            IAppDbContext db,
            IdentityErrorDescriber describer) : base((AppDbContext)db, describer) {
            _db = db;
            _describer = describer;
        }

        #region BaseClass

        protected override RoleClaim CreateRoleClaim(Role role, Claim claim) {
            return new RoleClaim
            {
                RoleId = role.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            };
        }

        public string ConvertIdToString(int id) {
            throw new NotImplementedException();
        }

        int IAppRoleStore.ConvertIdFromString(string id) {
            throw new NotImplementedException();
        }

        #endregion

        #region CustomMethods

        #endregion
    }
}
