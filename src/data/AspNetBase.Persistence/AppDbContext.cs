using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AspNetBase.Persistence.Configuration;
using AspNetBase.Domain.Models;
using AspNetBase.Core.Configuration;
using AspNetBase.Contracts.Persistence;
using Microsoft.Extensions.Configuration;

namespace AspNetBase.Persistence {

    public class AppDbContext 
        : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
          IAppDbContext {

        private IDbContextTransaction _transaction;
        //private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions options) : base(options) {
            //_configuration = configuration;
        }

        #region Repositories


        #endregion

        #region Interface Methods

        public IDbContextTransaction GetCurrentTransaction()
            => _transaction;

        public bool HasActiveTransaction => _transaction != null;

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
            => Set<TEntity>().AddRange(entities);

        public void BeginTransaction() 
            => _transaction = Database.BeginTransaction();
        
        public void CommitTransaction() {
            if (_transaction == null) {
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");
            }
            _transaction.Commit();
        }

        public void RollbackTransaction() {
            if (_transaction == null) {
                throw new NullReferenceException("Please call `BeginTransaction()` method first.");
            }
            _transaction.Rollback();
        }

        public override void Dispose() {
            _transaction?.Dispose();
            base.Dispose();
        }

        public int ExecuteSqlCommand(string query)
            => Database.ExecuteSqlRaw(query);

        public Task<int> ExecuteSqlCommandAsync(
            string query,
            params object[] parameters)
                => Database.ExecuteSqlRawAsync(query, parameters);

        public void ExecuteSqlInterpolatedCommand(FormattableString query)
            => Database.ExecuteSqlInterpolated(query);

        public void ExecuteSqlRawCommand(
            string query,
            params object[] parameters)
                => Database.ExecuteSqlRaw(query, parameters);

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
            => Update(entity);

        public void RemoveRange<TEntity>(
            IEnumerable<TEntity> entities
        ) where TEntity : class => Set<TEntity>().RemoveRange(entities);

        public override int SaveChanges(bool acceptAllChangesOnSuccess) {
            ChangeTracker.DetectChanges();
            beforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }


        public override int SaveChanges() {
            ChangeTracker.DetectChanges();
            beforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default) {
            ChangeTracker.DetectChanges();
            beforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = await base.SaveChangesAsync(
                acceptAllChangesOnSuccess,
                cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default) {
            ChangeTracker.DetectChanges();
            beforeSaveTriggers();
            ChangeTracker.AutoDetectChangesEnabled = false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = await base.SaveChangesAsync(cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public void SetAsNoTrackingQuery()
            => ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        public bool EnsureCreated()
            => base.Database.EnsureCreated();

        public void MigrateDb()
            => base.Database.Migrate();

        public async Task MigrateDbAsync(CancellationToken cancellationToken = default)
            => await base.Database.MigrateAsync(cancellationToken);

        #endregion

        #region Private Methods

        private void beforeSaveTriggers() {
            //this.CorrectYeKe();
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            var _configuration = Database.GetService<IConfiguration>();
            var _passwordHasher = Database.GetService<IPasswordHasher<User>>();

            var roleSeed = new RoleSeedData
            {
                Name = _configuration["Seed:RoleSeed:Name"],
                Title = _configuration["Seed:RoleSeed:Title"]
            };

            var userSeed = new UserSeedData
            {
                Email = _configuration["Seed:UserSeed:Email"],
                FirstName = _configuration["Seed:UserSeed:FirstName"],
                LastName = _configuration["Seed:UserSeed:LastName"],
                NationalCode = _configuration["Seed:UserSeed:NationalCode"],
                Password = _configuration["Seed:UserSeed:Password"],
                PhoneNumber = _configuration["Seed:UserSeed:PhoneNumber"],
                UserName = _configuration["Seed:UserSeed:UserName"]
            };

            Console.WriteLine($"RoleName : {roleSeed.Name}");

            #region Identity mappings

            builder.ConfigRole(roleSeed, _passwordHasher);
            builder.ConfigUser(userSeed, _passwordHasher);
            builder.ConfigRoleClaim();
            builder.ConfigAppClaimType();
            builder.ConfigUserClaim();
            builder.ConfigUserLoign();
            builder.ConfigUserRole();
            builder.ConfigUserToken();
            builder.ConfigUserUsedPassword();
            
            #endregion
        }
    }
}
