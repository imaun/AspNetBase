using AspNetBase.Domain.Models;

namespace AspNetBase.Contracts.Identity {

    public interface IAppUserClaimManager {

        Task ReplaceClaimAsync(User user, string claimType, string claimValue);

    }
}
