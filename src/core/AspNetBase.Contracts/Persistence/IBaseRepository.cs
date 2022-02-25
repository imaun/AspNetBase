using System.Linq.Expressions;

namespace AspNetBase.Contracts.Persistence {

    public interface IBaseRepository<TEntity, TKey> where TEntity : class {

        void Add(TEntity entity);

        Task AddAsync(TEntity entity, 
            CancellationToken cancellationToken = default);

        Task AddThenSaveAsync(TEntity entity, 
            CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        void UpdateThenSave(TEntity entity);

        Task UpdateThenSaveAsync(TEntity entity, 
            CancellationToken cancellationToken = default);

        void Delete(TEntity entity);

        void DeleteThenSave(TEntity entity);

        Task DeleteThenSaveAsync(TEntity entity, 
            CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default);

        TEntity Find(TKey id);

        Task<TEntity> UntrackedFindAsync(TKey id);

        Task<TEntity> FindAsync(TKey id);

        IQueryable<TEntity> Query();

        TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default);

        Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default);

    }

}
