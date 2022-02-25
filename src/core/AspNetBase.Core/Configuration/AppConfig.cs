namespace AspNetBase.Core.Configuration
{

    public class AppConfig
    {

        public DbConfig Database { get; set; }

        public AppSeedData Seed { get; set; }

        public AppIdentityOptions Identity { get; set; }

        public ViewOptions View { get; set; }
    }
}
