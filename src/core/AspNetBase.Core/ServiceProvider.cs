using Microsoft.Extensions.Options;
using AspNetBase.Core.Configuration;
using AspNetBase.Core;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceProvider {

        public static void AddAuditingCore(this IServiceCollection services) {
            services.AddHttpContextAccessor();
            services.AddScoped<IAppHttpContext, AppHttpContext>();
            services.AddScoped<IUserContext, UserContext>();
        }

        public static void AddAuditingConfiguration(this IServiceCollection services, AppConfig config) {
            services.AddSingleton<AppConfig>(config);
        }

        public static AppConfig GetApplicationConfiguration(this IServiceCollection services) {
            var provider = services.BuildServiceProvider();
            var config = provider.GetRequiredService<IOptionsSnapshot<AppConfig>>();
            var result = config.Value;

            if (result == null)
                throw new NullReferenceException("AppConfig is null.");

            return result;
        }

    }
}
