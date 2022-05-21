using AspNetBase.Persistence;
using AspNetBase.Domain.Models;
using AspNetBase.Contracts.Repository;
using AspNetBase.Contracts.Persistence;

namespace AspNetBase.Repository {

    public class UserRepository : BaseRepository<User, int>, IUserRepository {

        protected UserRepository(IAppDbContext db) : base(db) { }


        public async Task<User> GetByIdAsync(long id) {
            
        }
    }

}
