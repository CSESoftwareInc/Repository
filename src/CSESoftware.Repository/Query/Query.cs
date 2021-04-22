using CSESoftware.Repository.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace CSESoftware.Repository.Query
{
    public class Query<T> : IQuery<T> where T : class
    {
        public Query()
        {
            CancellationToken = CancellationToken.None;
            Include = new List<Expression<Func<T, object>>>();
            Ordering = new List<Ordering<T>>();
        }

        public Expression<Func<T, bool>> Predicate { get; set; }
        public List<Expression<Func<T, object>>> Include { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public CancellationToken CancellationToken { get; set; }

        internal List<Ordering<T>> Ordering;
        private Func<IQueryable<T>, IOrderedQueryable<T>> _orderBy;
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy {
            get
            {
                if (_orderBy != null) return _orderBy;

                if (!Ordering.Any()) return default;

                return x =>
                {
                    var orderBy = Ordering.First();
                    Ordering.Remove(orderBy);

                    var ordering = orderBy.Descending
                        ? x.OrderByDescending(orderBy.OrderBy)
                        : x.OrderBy(orderBy.OrderBy);

                    foreach (var thenBy in Ordering)
                    {
                        ordering = thenBy.Descending
                            ? ordering.ThenByDescending(thenBy.OrderBy)
                            : ordering.ThenBy(thenBy.OrderBy);
                    }

                    return ordering;
                };
            }
            set => _orderBy = value;
        }
    }
}
