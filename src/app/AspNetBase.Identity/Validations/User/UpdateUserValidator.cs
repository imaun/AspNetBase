using FluentValidation;
using AspNetBase.Resources;
using AspNetBase.Identity.Commands;

namespace AspNetBase.Identity.Validations {

    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand> {

        public UpdateUserValidator() {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(IdentityStrings.Req_User_FirstName);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(IdentityStrings.Req_User_LastName);
            RuleFor(x => x.UserName).NotEmpty().WithMessage(IdentityStrings.Req_User_UserName);
            RuleFor(x => x.NationalCode).NotEmpty().WithMessage(IdentityStrings.Req_User_NationalCode);
        }
    }
}
