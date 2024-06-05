using Microsoft.EntityFrameworkCore.Storage;

namespace StreamingApp.Domain.Common;

// TODO: Do i need IDisposable
public interface IUnitOfWork
{
    Task<TEntity> AddEntityAsync<TEntity>(TEntity entity) where TEntity : class;

    IDbContextTransaction BeginTransaction();

    Task CommitTransactionAsync();

    void Delete<TEntity>(TEntity entity) where TEntity : class;

    void DeleteMany<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    IQueryable<TEntity> EntitySet<TEntity>() where TEntity : class;

    Task SaveChangesAsync();
}
