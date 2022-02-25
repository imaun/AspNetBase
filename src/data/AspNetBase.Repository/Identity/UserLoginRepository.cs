using AspNetBase.Persistence;
using AspNetBase.Domain.Models;
using AspNetBase.Contracts.Persistence;
using AspNetBase.Contracts.Repository;

namespace AspNetBase.Repository {

    public class UserLoginRepository : BaseRepository<UserLogin, int>, IUserLoginRepository {

        protected UserLoginRepository(IAppDbContext db) : base(db) { }

    }
}
