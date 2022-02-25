using AspNetBase.Domain.Models;
using AspNetBase.Contracts.Persistence;

namespace AspNetBase.Contracts.Repository {

    public interface IUserLoginRepository : IBaseRepository<UserLogin, int> {

    }
}
