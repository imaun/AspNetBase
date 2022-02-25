using MediatR;
using Datiss.Common.Utils;
using Datiss.Common.Gaurd;
using AspNetBase.Contracts.Identity;
using AspNetBase.Core.Configuration;
using AspNetBase.Identity.Validations;
using AspNetBase.Identity.Models;
using AspNetBase.Extensions;
using AspNetBase.Contracts.Persistence;
using Datiss.Common.Identity;
using AspNetBase.Domain.Models;
using Datiss.Common.Persian;
using Mapster;

namespace AspNetBase.Identity.Commands {

    public class UpdateUserCommand : IRequest<Result<UpdateUserResult>>
    {
        public UpdateUserCommand(int id, string userName, string firstName, string lastName, string nationalCode, string phoneNumber) {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            NationalCode = nationalCode;
            PhoneNumber = phoneNumber;
        }

        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string NationalCode { get; private set; }
        public string PhoneNumber { get; private set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UpdateUserResult>>
    {

        private readonly IAppUserManager _userManager;
        private readonly UpdateUserValidator _validator;
        private readonly IAuditingDbContext _db;

        public UpdateUserCommandHandler(
            IAppUserManager userManager,
            UpdateUserValidator validator,
            IAuditingDbContext db) {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Result<UpdateUserResult>> Handle(UpdateUserCommand command, CancellationToken cancellationToken) {
            command.CheckArgumentIsNull(nameof(command));

            var logic = await _validator.ValidateAsync(command, cancellationToken);
            if(!logic.IsValid) {
                return Result<UpdateUserResult>
                    .Failed(null, message: logic.Errors?.ExtractMessages());
            }

            var user = await _db.Set<User>().FindAsync(command.Id);
            user.CheckReferenceIsNull(nameof(user));

            user.FirstName = command.FirstName.Trim().ApplyCorrectYeKe();
            user.LastName = command.LastName.Trim().ApplyCorrectYeKe();
            user.NationalCode = command.NationalCode.ToEnglishNumbers();
            user.PhoneNumber = command.PhoneNumber.ToEnglishNumbers();
            user.UserName = command.UserName.Trim();

            _db.Set<User>().Update(user);
            await _db.SaveChangesAsync(cancellationToken);

            return Result<UpdateUserResult>
                .Succeed(command.Adapt<UpdateUserResult>());
        }

    }
}
