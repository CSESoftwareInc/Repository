using System;
using System.Linq.Expressions;

namespace CSESoftware.Repository.Query
{
    public class SelectQuery<T, TOut> :  Query<T>, ISelectQuery<T, TOut>
        where T : class
    {
        public SelectQuery()
        {
        }

        public SelectQuery(IQuery<T> query)
        {
            Predicate = query.Predicate;
            Order = query.Order;
            Include = query.Include;
            Skip = query.Skip;
            Take = query.Take;
            CancellationToken = query.CancellationToken;
        }

        public Expression<Func<T, TOut>> Select { get; set; }
    }
}
