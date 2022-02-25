namespace AspNetBase.Domain.Models {

    public class UserUsedPassword {

        public UserUsedPassword() { }

        #region Properties

        public int Id { get; set; }

        public string HashedPassword { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        #endregion
    }
}
