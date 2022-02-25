using AspNetBase.Enum;
using AspNetBase.Core.Models;

namespace AspNetBase.Identity.Models
{
    public class UserResult
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
    }

    public class UserListItemResult
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public string StatusDisplay => Status.Title;
    }

    public class UserListResult : PagedResult<UserListItemResult>
    {

    }

}
