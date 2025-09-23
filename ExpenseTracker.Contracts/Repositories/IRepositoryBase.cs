using System.Linq.Expressions;
using ExpenseTracker.Shared.Contracts;

namespace ExpenseTracker.Contracts.Repositories;

public interface IRepositoryBase<T> where T : class, IEntity
{
    public IQueryable<T> FindByCondition(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool trackChanges = true,
        Func<IQueryable<T>, IQueryable<T>>? include = null);

    IQueryable<T> FindByConditionDynamic
    (
        IEnumerable<string>? filters = null,
        string? orderBy = null,
        string? sortOrder = null,
        bool trackChanges = true,
        Func<IQueryable<T>,
        IQueryable<T>>? include = null
    );

    Task Add(T entity, CancellationToken cancellation = default);

    void Update(T entity);

    void Delete(T entity);


    // TODO: decide on these later
    //Task<T?> GetById(CancellationToken cancellationToken, params object[] keyValues);

    //Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellation = default);

    //Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellation = default);
}
