using System.Security.Claims;

namespace AspNetBase.Contracts.Identity {

    /// <summary>
    /// Check User access based on policy name.
    /// </summary>
    public interface IAppSecurityCheckService {

        /// <summary>
        /// Check if the logged-in User has access to specefied route.
        /// </summary>
        /// <param name="action">Action name</param>
        /// <param name="controller">Controller name</param>
        /// <param name="area">Area name, send empty string or null if does not have an area</param>
        /// <param name="policy">The policy name to check.</param>
        /// <returns>True if has access or False if not.</returns>
        bool CurrentUserHasAccessTo(
            string action,
            string controller,
            string area = "",
            string policy = "dynamic");


        /// <summary>
        /// Check if a User has access to specefied route.
        /// </summary>
        /// <param name="user">The User to check</param>
        /// <param name="action">Action name</param>
        /// <param name="controller">Controller name</param>
        /// <param name="area">Area name, send empty string or null if does not have an area</param>
        /// <param name="policy">The policy name to check.</param>
        /// <returns>True if has access or False if not.</returns>
        bool UserHasAccessTo(
            ClaimsPrincipal user,
            string action,
            string controller,
            string area = "",
            string policy = "dynamic");
    }
}
