using System.Security.Claims;
using AspNetBase.Domain.Models;

namespace AspNetBase.Contracts.Identity {

    public interface IUserAuthenticationService {

        Task<bool> SignInAsync(
           string username,
           string password,
           bool rememberMe = false);

        Task<bool> TwoFactorSingInAsync(
            string token,
            bool rememberMe = false);

        Task ResendTwoFactorCodeAsync(string username);

        Task SignOut(ClaimsPrincipal user);

        Task<string> GeneratePhoneNumberTokenAsync(
            User user,
            string phoneNumber = null);

        Task SendPhoneNumberVerificationTokenAsync(string phoneNumber);

        Task SendPhoneNumberVerificationTokenAsync(User user);

        Task VerifyPhoneNumberTokenAsync(User user, string token);

        Task VerifyPhoneNumberTokenAsync(long userId, string token);

        Task VerifyPhoneNumberTokenAsync(string userName, string token);

        Task SendForgetPasswordTokenAsync(string phoneNumber);

        Task<string> VerifyForgetPasswordTokenAsync(
            string phoneNumber,
            string token);

        Task ResetPasswordAsync(
            string phoneNumber,
            string token,
            string newPassword);

        Task<string> GenerateEmailConfirmationTokenAsync(long userId);

        Task ConfirmEmailAsync(long userId, string token);
    }
}
