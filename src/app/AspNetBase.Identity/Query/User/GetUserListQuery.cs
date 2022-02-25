using Microsoft.EntityFrameworkCore;
using Datiss.Common.Gaurd;
using AspNetBase.Enum;
using AspNetBase.Core.Models;
using AspNetBase.Domain.Models;
using AspNetBase.Identity.Models;
using AspNetBase.Domain.Extensions;
using AspNetBase.Contracts.Persistence;
using Datiss.Common.Persian;
using MediatR;
using Mapster;

namespace AspNetBase.Identity.Query {

    public class GetUserListQuery : QueryFilterModel, IRequest<UserListResult> {

        public GetUserListQuery(int pageNum, int pageSize, string orderBy, bool orderDesc = false, string search = "") {
            PageNumber = pageNum;
            PageSize = pageSize;
            OrderBy = orderBy;
            OrderDesc = orderDesc;
            Search = search;
        }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Unknown;
    }

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserListResult>
    {
        private readonly IAuditingDbContext _db;

        public GetUserListQueryHandler(IAuditingDbContext db) {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<UserListResult> Handle(GetUserListQuery request, CancellationToken cancellationToken) {
            request.CheckArgumentIsNull(nameof(request));

            var result = new UserListResult();

            var query = _db.Set<User>().AsNoTracking();
            if(request.UserName.IsNotNullOrEmpty()) {
                query = query.Where(_ => _.UserName.Contains(request.UserName));
            }
            if(request.FullName.IsNotNullOrEmpty()) {
                request.FullName = request.FullName.Trim().ApplyCorrectYeKe();
                query = query.Where(_ => _.FirstName.Contains(request.FullName) ||
                                            _.LastName.Contains(request.FullName));
            }
            if(request.PhoneNumber.IsNotNullOrEmpty()) {
                query = query.Where(_ => _.PhoneNumber.Contains(request.PhoneNumber));
            }
            if(request.NationalCode.IsNotNullOrEmpty()) {
                query = query.Where(_=> _.NationalCode.Contains(request.NationalCode));
            }
            if(request.Status.IsNotNullOrUnknown) {
                query = query.Where(_ => _.Status == request.Status);
            }

            result.TotalCount = await query.CountAsync();
            
            query = query
                .SetOrder(request.OrderBy, request.OrderDesc)
                .Skip(request.StartIndex)
                .Take(request.PageSize);

            result.Items = await query
                .Select(_ => _.Adapt<UserListItemResult>())
                .ToListAsync();

            return await Task.FromResult(result);
        }

    }

}
