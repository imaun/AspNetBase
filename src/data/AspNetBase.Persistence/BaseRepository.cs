using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AspNetBase.Contracts.Persistence;

namespace AspNetBase.Persistence {

    public class BaseRepository<TEntity, TKey> 
        : IBaseRepository<TEntity, TKey> where TEntity : class {

        protected readonly IAppDbContext _db;
        protected readonly DbSet<TEntity> _repo;

        protected BaseRepository(IAppDbContext db) {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void Add(TEntity entity)
            => _repo.Add(entity);

        public void AddThenSave(TEntity entity) {
            Add(entity);
            _db.SaveChanges();
        }

        public async Task AddAsync(TEntity entity, 
            CancellationToken cancellationToken = default)
                => await _repo.AddAsync(entity, cancellationToken);

        public async Task AddThenSaveAsync(TEntity entity, 
            CancellationToken cancellationToken = default) {
            await AddAsync(entity, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(TEntity entity)
            => _repo.Update(entity);

        public void UpdateThenSave(TEntity entity) {
            Update(entity);
            _db.SaveChanges();
        }

        public async Task UpdateThenSaveAsync(TEntity entity, 
            CancellationToken cancellationToken = default) {
            Update(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Delete(TEntity entity)
            => _repo.Remove(entity);

        public void DeleteThenSave(TEntity entity) {
            Delete(entity);
            _db.SaveChanges();
        }

        public async Task DeleteThenSaveAsync(
            TEntity entity, 
            CancellationToken cancellationToken = default) {
            Delete(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default) => await _repo.AnyAsync(predicate);

        public TEntity Find(TKey id)
            => _repo.Find(id);

        public async Task<TEntity> FindAsync(TKey id) 
            => await _repo.FindAsync(id);

        public async Task<TEntity> UntrackedFindAsync(TKey id) {
            var entity = await _repo.FindAsync(id);
            _db.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public IQueryable<TEntity> Query() => _repo.AsNoTracking();

        public async Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default) 
                => await _repo.FirstOrDefaultAsync(predicate, cancellationToken);

        public TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> predicate)
                => _repo.SingleOrDefault(predicate);
        
        public async Task<TEntity> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default) 
                => await _repo.SingleOrDefaultAsync(predicate, cancellationToken);
        
    }

}
