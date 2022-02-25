using MediatR;

namespace AspNetBase.Domain.Events {

    public class UserLoggedInEventData : INotification {

        public UserLoggedInEventData(
            int userId,
            string userName = "",
            string displayName = "") {
            UserId = userId;
            UserName = userName;
            DisplayName = displayName;
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }

    }
}
