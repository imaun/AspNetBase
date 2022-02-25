using Microsoft.EntityFrameworkCore;
using AspNetBase.Identity.Models;
using AspNetBase.Domain.Models;
using AspNetBase.Contracts.Persistence;
using Datiss.Common.Gaurd;
using MediatR;

namespace AspNetBase.Identity.Query {

    public class GetAppClaimTypesQuery : IRequest<IEnumerable<AppClaimTypeResult>>
    {

    }

    public class GetAppClaimTypeQueryHandler : IRequestHandler<GetAppClaimTypesQuery, IEnumerable<AppClaimTypeResult>>
    {
        private readonly IAuditingDbContext _db;

        public GetAppClaimTypeQueryHandler(IAuditingDbContext db) {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<AppClaimTypeResult>> Handle(
            GetAppClaimTypesQuery request, 
            CancellationToken cancellationToken) {

            request.CheckArgumentIsNull(nameof(request));

            var result = new List<AppClaimTypeResult>();

            var query = _db.Set<AppClaimType>().AsNoTracking();
            result = await query.OrderBy(_ => _.Id)
                                .Select(_ => new AppClaimTypeResult
                                {
                                    Id = _.Id,
                                    Name = _.Name,
                                    Title = _.Title,
                                    Description = _.Description
                                }).ToListAsync();

            return await Task.FromResult(result);
        }

    }
}
