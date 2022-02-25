using MediatR;
using Datiss.Common.Utils;
using Datiss.Common.Gaurd;
using AspNetBase.Contracts.Identity;
using AspNetBase.Core.Configuration;
using AspNetBase.Identity.Validations;
using AspNetBase.Identity.Models;
using AspNetBase.Extensions;
using Datiss.Common.Identity;
using AspNetBase.Domain.Models;

namespace AspNetBase.Identity.Commands {

    public class CreateUserCommand : IRequest<Result<CreateUserResult>> {

        public CreateUserCommand(
            string userName,
            string firstName,
            string lastName,
            string nationalCode) {

            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
        }

        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NationalCode { get; private set; }

    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserResult>> {

        private readonly IAppUserManager _userManager;
        private readonly AppConfig _appConfig;
        private readonly CreateUserValidator _validator;

        public CreateUserCommandHandler(
            IAppUserManager userManager,
            AppConfig appConfig,
            CreateUserValidator validator) {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _appConfig = appConfig ?? throw new ArgumentNullException(nameof(appConfig));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<Result<CreateUserResult>> Handle(CreateUserCommand command, CancellationToken cancellationToken) {
            command.CheckArgumentIsNull(nameof(command));

            var logic = await _validator.ValidateAsync(command);
            if(!logic.IsValid) {
                return Result<CreateUserResult>
                    .Failed(null, message: logic.Errors?.ExtractMessages());
            }
            
            var user = new User
            {
                UserName = command.UserName,
                FirstName = command.FirstName,
                LastName = command.LastName,
                NationalCode = command.NationalCode
            };

            try {
                var result = await _userManager.CreateAsync(user, _appConfig.Identity.DefaultUserPassword);
                if(!result.Succeeded) {
                    //result.Errors.
                    //TODO : 
                    return Result<CreateUserResult>
                        .Failed(null, message: "خطا در ایجاد کاربر.");

                }
            }
            catch(Exception ex) {
                return Result<CreateUserResult>.Failed(null);
            }


            return Result<CreateUserResult>.Succeed(new CreateUserResult
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                NationalCode = command.NationalCode,
                UserName = command.UserName
            }, "");
        }

    }

}
