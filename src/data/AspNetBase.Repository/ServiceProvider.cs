using AspNetBase.Repository;
using AspNetBase.Contracts.Repository;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceProvider {

        public static  IServiceCollection AddAuditingRepositories(
            this IServiceCollection services) {

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();

            return services;
        }

    }
}
