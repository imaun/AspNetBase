using Microsoft.AspNetCore.Identity;
using Datiss.Common.Utils;
using Datiss.Common.Gaurd;
using AspNetBase.Resources;
using AspNetBase.Extensions;
using AspNetBase.Domain.Events;
using AspNetBase.Identity.Models;
using AspNetBase.Contracts.Identity;
using AspNetBase.Identity.Validations;
using MediatR;
using AspNetBase.Domain.Models;

namespace AspNetBase.Identity.Commands
{

    public class UserLoginCommand : IRequest<Result<UserLoginResult>>
    {

        public UserLoginCommand(
            string userName,
            string password,
            bool rememberMe = false,
            bool lockedOutWhenFailed = false) {

            UserName = userName;
            Password = password;
            RememberMe = rememberMe;
            LockedOutWhenFailed = lockedOutWhenFailed;
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public bool RememberMe { get; private set; }
        public bool LockedOutWhenFailed { get; private set; }
    }

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<UserLoginResult>>
    {
        //private readonly IAppSignInManager _signInManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAppUserManager _userManager;
        private readonly UserLoginValidator _validator;
        private readonly IMediator _mediator;

        public UserLoginCommandHandler(
            //IAppSignInManager signInManager,
            SignInManager<User> signInManager,
            IAppUserManager userManager,
            UserLoginValidator validator,
            IMediator mediator) {

            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Result<UserLoginResult>> Handle(UserLoginCommand command, CancellationToken cancellationToken = default) {
            command.CheckArgumentIsNull(nameof(command));

            var logic = _validator.Validate(command);
            if(!logic.IsValid) {
                return Result<UserLoginResult>
                    .Failed(default, message: logic.Errors?.ExtractMessages());
            }

            var user = await _userManager.FindByNameAsync(command.UserName);
            if (user == null) {
                return Result<UserLoginResult>.Failed(
                    default, 
                    IdentityStrings.Err_Login_Invalid);
            }

            if(user.Status.NotValidForLogin) {
                return Result<UserLoginResult>.Failed
                    (default, IdentityStrings.Err_Login_StatusInvalid);
            }

            if(user.Status.IsBlocked) {
                return Result<UserLoginResult>
                    .Failed(default, message: IdentityStrings.Err_Login_Blocked);
            }

            var loginRes = await _signInManager.PasswordSignInAsync(
                user,
                command.Password,
                command.RememberMe,
                command.LockedOutWhenFailed);

            if(!loginRes.Succeeded) {
                if(loginRes.RequiresTwoFactor) {
                    var twoFactorResult = new UserLoginResult
                    {
                        RequiresTwoFactor = true,
                        TwoFactorToken = await _userManager.GenerateTwoFactorTokenAsync(user, "sms"),
                        UserId = user.Id
                    };

                    return Result<UserLoginResult>.Succeed(
                        twoFactorResult, 
                        IdentityStrings.Err_Login_RequireTwoFactor);
                }

                var failedResult = new UserLoginResult
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName
                };

                return Result<UserLoginResult>.Failed(
                    failedResult,
                    IdentityStrings.Err_Login_Invalid);
            }

            await _mediator.Publish(
                new UserLoggedInEventData(user.Id, user.UserName, user.DisplayName), 
                cancellationToken);

            return Result<UserLoginResult>.Succeed(new UserLoginResult
            {
                UserId = user.Id,
                DisplayName = user.DisplayName,
                UserName = user.UserName
            }, message: IdentityStrings.Ok_Login);
        }

    }

}
