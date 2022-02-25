using AspNetBase.Resources;

namespace AspNetBase.Core.Enum {

    /// <summary>
    /// Determines the <see cref="User"/>'s status
    /// </summary>
    public class UserStatus : Enumeration {

        public UserStatus(int id, string name, string title = "") 
            : base(id, name, title) { }

        public static UserStatus Unknown
            => new UserStatus(-100, nameof(Unknown), EnumStrings.Unknown);

        public static UserStatus Deleted 
            => new UserStatus(-1, nameof(Deleted), EnumStrings.Deleted);

        public static UserStatus Disabled 
            => new UserStatus(0, nameof(Disabled), EnumStrings.Disabled);

        public static UserStatus Enabled 
            => new UserStatus(1, nameof(Enabled), EnumStrings.Enabled);

        public static UserStatus Blocked 
            => new UserStatus(2, nameof(Blocked), EnumStrings.UserStatus_Blocked);

        public static UserStatus Find(int id)
            => FromValue<UserStatus>(id);

        public bool NotValidForLogin 
            => (Id == Deleted.Id || Id == Disabled.Id || Id == Blocked.Id);

        public bool ValidForLogin => !NotValidForLogin;

        public bool IsBlocked => Id == Blocked.Id;

        public bool IsDeleted => Id == Deleted.Id;

        public bool IsDisabled => Id == Disabled.Id;

        public bool IsEnabled => Id == Enabled.Id;
        
        public bool IsUnknown => Id == Unknown.Id;

        public bool IsNotNullOrUnknown
            => this != null && !IsUnknown;

    }

}
