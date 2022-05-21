using FluentValidation;
using AspNetBase.Resources;
using AspNetBase.Identity.Commands;

namespace AspNetBase.Identity.Validations
{

    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {

        public CreateUserValidator() {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(IdentityStrings.Req_User_FirstName);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(IdentityStrings.Req_User_LastName);
            RuleFor(x => x.UserName).NotEmpty().WithMessage(IdentityStrings.Req_User_UserName);
        }

    }
}
