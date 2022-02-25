namespace Datiss.Auditing.Web
{
    public class Startup
    {
        public Startup(IConfigurationRoot configuration) {
            Configuration = configuration;
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDatissCommon();
            
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime) {
            // ...
        }
    }
}
