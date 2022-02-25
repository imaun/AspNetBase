using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using AspNetBase.Domain.Models;
using AspNetBase.Core.Configuration;

namespace AspNetBase.Persistence.SqlServer {

    public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
    {
        public SqlServerDbContext CreateDbContext(string[] args) {
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddIdentityCore<User>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();

            var basePath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Using '{basePath}' as the ContentRootPath");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", false, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfigurationRoot>(provider => configuration);
            services.Configure<AppConfig>(options => configuration.Bind(options));
            var provider = services.BuildServiceProvider();
            Console.WriteLine("Configuration Binded........");
            var config = provider.GetRequiredService<IOptionsSnapshot<AppConfig>>().Value;
            var passwordHasher = provider.GetRequiredService<IPasswordHasher<User>>();
            Console.WriteLine($"Using ConnectionString: {config.Database.SqlServerConnection}");
            services.AddEntityFrameworkSqlServer();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.Configure(config.Database, services.BuildServiceProvider());
            //var myconfig = provider.GetRequiredService<IConfiguration>();

            Console.WriteLine("DbContext Created.");

            //return new SqlServerDbContext(optionsBuilder.Options, configuration);
            return new SqlServerDbContext(optionsBuilder.Options);
        }
    }
}
