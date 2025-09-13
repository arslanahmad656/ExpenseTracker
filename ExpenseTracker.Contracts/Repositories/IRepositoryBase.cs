using System.Linq.Expressions;

namespace ExpenseTracker.Contracts.Repositories;

public interface IRepositoryBase<T> where T : class, IEntity
{
    IQueryable<T> FindByCondition(Expression<Func<T, bool>>? filter = null,
                                   Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                   bool trackChanges = true,
                                   params Expression<Func<T, object>>[] includes);

    Task Add(T entity, CancellationToken cancellation = default);

    void Update(T entity);

    void Delete(T entity);


    // TODO: decide on these later
    //Task<T?> GetById(CancellationToken cancellationToken, params object[] keyValues);

    //Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellation = default);

    //Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation = default);
}
