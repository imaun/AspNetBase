namespace AspNetBase.Core.Configuration
{
    public class AppSeedData
    {
        public UserSeedData UserSeed { get; set; }
        public RoleSeedData RoleSeed { get; set; }
    }

    public class UserSeedData
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RoleSeedData
    {
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
