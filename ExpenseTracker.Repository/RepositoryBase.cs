using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository;

public abstract class RepositoryBase<T> (ExpenseTrackerDbContext repositoryContext) : IRepositoryBase<T> where T : class, IEntity
{
    private readonly DbSet<T> set = repositoryContext.Set<T>();
    protected ExpenseTrackerDbContext RepositoryContext => repositoryContext;

    public Task Add(T entity, CancellationToken cancellationToken = default) => set.AddAsync(entity, cancellationToken).AsTask();

    public void Delete(T entity) => set.Remove(entity);

    //public IQueryable<T> FindByCondition(Expression<Func<T, bool>>? filter = null,
    //                               Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    //                               bool trackChanges = true,
    //                               params Expression<Func<T, object>>[] includes)
    //{
    //    IQueryable<T> query = set.AsQueryable();

    //    if (filter != null)
    //    {
    //        query = query.Where(filter);
    //    }

    //    foreach (var include in includes)
    //    {
    //        query = query.Include(include);
    //    }

    //    if (orderBy != null)
    //    {
    //        query = orderBy(query);
    //    }

    //    if (!trackChanges)
    //    {
    //        query = query.AsNoTracking();
    //    }

    //    return query;
    //}

    public IQueryable<T> FindByCondition(
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    bool trackChanges = true,
    Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        IQueryable<T> query = set.AsQueryable();

        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return query;
    }

    public IQueryable<T> FindByConditionDynamic
    (
        IEnumerable<string>? filters = null,
        string? orderBy = null,
        string? sortOrder = null,
        bool trackChanges = true,
        Func<IQueryable<T>, 
        IQueryable<T>>? include = null
    )
    {
        IQueryable<T> query = set.AsQueryable();

        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (filters != null)
        {
            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }
        }

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var effectiveOrderBy = orderBy;
            if (sortOrder is not null)
            {
                effectiveOrderBy += $" {sortOrder}";
            }

            query = query.OrderBy(effectiveOrderBy);
        }

        return query;
    }



    public void Update(T entity)
    {
        set.Attach(entity);
        RepositoryContext.Entry(entity).State = EntityState.Modified;
    }

    //public Task<T?> GetById(CancellationToken cancellationToken, params object[] keyValues) => set.FindAsync(keyValues, cancellationToken).AsTask();

    //public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) => set.AnyAsync(predicate, cancellationToken);

    //public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    //    => predicate is null ? set.CountAsync(cancellationToken) : set.CountAsync(predicate, cancellationToken);
}
