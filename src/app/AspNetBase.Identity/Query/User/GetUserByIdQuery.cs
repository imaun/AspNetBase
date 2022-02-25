using AspNetBase.Identity.Models;
using Datiss.Common.Gaurd;
using AspNetBase.Contracts.Persistence;
using AspNetBase.Contracts.Identity;
using MediatR;
using Mapster;

namespace AspNetBase.Identity.Query
{
    public class GetUserByIdQuery : IRequest<UserResult>
    {

        public GetUserByIdQuery(int id) {
            Id = id;
        }

        public int Id { get; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResult>
    {

        private readonly IAppUserManager _userManager;
        
        public GetUserByIdQueryHandler(IAppUserManager userManager) {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<UserResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) {
            request.CheckArgumentIsNull(nameof(request));

            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            user.CheckReferenceIsNull(nameof(user));

            return user.Adapt<UserResult>();
        }

    }
}
