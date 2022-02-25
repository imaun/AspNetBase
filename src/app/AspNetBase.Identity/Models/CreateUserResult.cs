namespace AspNetBase.Identity.Models {

    public class CreateUserResult {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";
        public string NationalCode { get; set; }
    }
}
