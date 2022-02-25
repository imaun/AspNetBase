using FluentValidation;
using AspNetBase.Identity.Commands;
using AspNetBase.Resources;

namespace AspNetBase.Identity.Validations {

    public class UserLoginValidator : AbstractValidator<UserLoginCommand> {

        public UserLoginValidator() {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(IdentityStrings.Req_Login_UserName);
            RuleFor(x => x.Password).NotEmpty().WithMessage(IdentityStrings.Req_Login_Password);
        }

    }
}
