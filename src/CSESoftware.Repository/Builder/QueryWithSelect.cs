using System;
using System.Linq.Expressions;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository.Builder
{
    public class QueryWithSelect<TEntity, TOut> :  Query<TEntity>, IQueryWithSelect<TEntity, TOut> where TEntity : class, IEntity
    {
        public QueryWithSelect(IQuery<TEntity> query)
        {
            Predicate = query.Predicate;
            OrderBy = query.OrderBy;
            Include = query.Include;
            Skip = query.Skip;
            Take = query.Take;
            CancellationToken = query.CancellationToken;
        }

        public Expression<Func<TEntity, TOut>> Select { get; set; }
    }
}
