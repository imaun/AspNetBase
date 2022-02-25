using Datiss.Common.Gaurd;
using AspNetBase.Core;
using AspNetBase.Domain.Events;
using AspNetBase.Domain.Models;
using AspNetBase.Contracts.Repository;
using AspNetBase.Contracts.Persistence;
using MediatR;

namespace Datiss.Auditing.Identity.Events
{

    public class UserLoggedInEventHandler : INotificationHandler<UserLoggedInEventData>
    {
        private readonly IAuditingDbContext _db;
        //private readonly IUserLoginRepository _userLoginRepository;
        private readonly IAppHttpContext _appContext;
        private readonly IUserContext _userContext;

        public UserLoggedInEventHandler(
            IAuditingDbContext db,
            //IUserLoginRepository userLoginRepository,
            IAppHttpContext appContext,
            IUserContext userContext) {

            _db = db ?? throw new ArgumentNullException(nameof(db));
            //_userLoginRepository = userLoginRepository
            //    ?? throw new ArgumentNullException(nameof(userLoginRepository));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _appContext = appContext ?? throw new ArgumentNullException(nameof(appContext));
        }

        public async Task Handle(UserLoggedInEventData data, CancellationToken cancellationToken = default) {
            data.CheckArgumentIsNull(nameof(data));

            var login = new UserLogin
            {
                UserId = data.UserId,
                IpAddress = _appContext.IpAddress,
                LoginProvider = "[Auditing.Web]",
                UserAgent = _appContext.UserAgent,
            };

            //await _userLoginRepository.AddAsync(login, cancellationToken);
            //await _db.Set<UserLogin>().AddAsync(login, cancellationToken);
            //await _db.SaveChangesAsync(cancellationToken);
        }

    }
}
