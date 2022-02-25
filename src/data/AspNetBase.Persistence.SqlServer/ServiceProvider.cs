using Microsoft.EntityFrameworkCore;
using AspNetBase.Persistence;
using AspNetBase.Core.Configuration;
using AspNetBase.Contracts.Persistence;
using AspNetBase.Persistence.SqlServer;

namespace Microsoft.Extensions.DependencyInjection {

    public static class ServiceProvider {

        public static IServiceCollection AddAuditingSqlServer(
           this IServiceCollection services,
           AppConfig config) {

            var provider = services.BuildServiceProvider();
            
            //services.AddTransient<IAuditingDbContext>(provider
            //    => provider.GetRequiredService<AuditingDbContext>());

            services.AddEntityFrameworkSqlServer();
            Console.WriteLine($"{config.Database.SqlServerConnection}");
            //services.AddDbContext<AuditingDbContext, SqlServerDbContext>(
            //    (provider, optionsBuilder)
            //        => { optionsBuilder.Configure(config.Database, provider); });
            services.AddDbContextPool<IAppDbContext, SqlServerDbContext>(
                (provider, optionsBuilder)
                    => { optionsBuilder.Configure(config.Database, provider); });

            return services;
        }

        public static void Configure(
            this DbContextOptionsBuilder optionsBuilder,
            DbConfig config,
            IServiceProvider provider) {

            optionsBuilder.UseSqlServer(config.SqlServerConnection,
                __ => {
                    __.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);
                    __.EnableRetryOnFailure();
                    __.MigrationsAssembly(typeof(SqlServerContextFactory).Assembly.FullName);
                });
            optionsBuilder.UseInternalServiceProvider(provider);
        }

    }
}
