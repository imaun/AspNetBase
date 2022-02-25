using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using AspNetBase.Core.Enum;

namespace AspNetBase.Domain.Models
{

    public class User : IdentityUser<int>, IBaseEntity
    {

        public User() 
        {
            initCollections();
        }

        #region Properties
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string DisplayName {
            get {
                var displayName = $"{FirstName} {LastName}";
                return string.IsNullOrWhiteSpace(displayName) ? UserName : displayName;
            }
        }

        public string NationalCode { get; set; }

        public UserStatus Status {
            get => UserStatus.FromValue<UserStatus>(_statusId);
            set => _statusId = value.Id;
        }
        private int _statusId { get; set; }

        #endregion

        #region Navigations

        public ICollection<UserClaim> Claims { get; set; }

        public ICollection<UserRole> Roles { get; set; }

        public ICollection<UserLogin> Logins { get; set; }

        public ICollection<UserUsedPassword> UsedPasswords { get; set; }

        public ICollection<UserToken> Tokens { get; set; }
        #endregion

        #region Private

        private void initCollections() {
            Claims = new HashSet<UserClaim>();
            Roles = new HashSet<UserRole>();
            Logins = new HashSet<UserLogin>();
            UsedPasswords = new HashSet<UserUsedPassword>();
            Tokens = new HashSet<UserToken>();
        }

        #endregion
    }
}
