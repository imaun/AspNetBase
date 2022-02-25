namespace AspNetBase.Core.Configuration
{
    public class CookieOptions
    {
        public string AccessDeniedPath { get; set; }
        public string CookieName { get; set; }
        public TimeSpan ExpireTimeSpan { get; set; }
        public string LoginPath { get; set; }
        public string LogoutPath { get; set; }
        public bool SlidingExpiration { get; set; }
        public bool UseDistributedCacheTicketStore { set; get; }
        public DistributedSqlServerCacheOptions DistributedSqlServerCacheOptions { set; get; }
        public DistributedSqliteCacheOptions DistributedSqliteCacheOptions { set; get; }

    }

    public class DistributedSqlServerCacheOptions
    {
        public string ConnectionString { set; get; }
        public string TableName { set; get; }
        public string SchemaName { set; get; }
    }

    public class DistributedSqliteCacheOptions
    {
        public string ConnectionString { set; get; }
    }
}
