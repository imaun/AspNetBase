using Microsoft.EntityFrameworkCore;

namespace AspNetBase.Persistence.SqlServer {

    public class SqlServerDbContext : AppDbContext {

        //private readonly IConfiguration configuration;

        public SqlServerDbContext(DbContextOptions options) : base(options) { }

    }
}