using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace CSESoftware.Repository.Query
{
    public interface IQuery<T> where T : class
    {
        Expression<Func<T, bool>> Predicate { get; set; }
        Func<IQueryable<T>, IOrderedQueryable<T>> Order { get; set; }
        List<Expression<Func<T, object>>> Include { get; set; }
        int? Skip { get; set; }
        int? Take { get; set; }
        CancellationToken CancellationToken { get; set; }
    }
}
