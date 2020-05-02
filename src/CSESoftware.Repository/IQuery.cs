using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IQuery<TEntity> where TEntity : class, IBaseEntity
    {
        Expression<Func<TEntity, bool>> Predicate { get; set; }
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }
        List<Expression<Func<TEntity, object>>> Include { get; set; }
        Expression<Func<TEntity, object>> Select { get; set; }
        int? Skip { get; set; }
        int? Take { get; set; }
        CancellationToken CancellationToken { get; set; }
    }
}