using CSESoftware.Repository.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSESoftware.Repository.Query.Extensions
{
    public static class OrderingExtensions
    {
        public static IQuery<T> OrderWith<T>(this IQuery<T> query, Func<IQueryable<T>, IOrderedQueryable<T>> ordering)
            where T : class
        {
            query.Order = ordering;
            return query;
        }

        public static Query<T> OrderBy<T>(this Query<T> query, Expression<Func<T, object>> orderBy)
            where T : class
        {
            query.Ordering = new List<Ordering<T>>
            {
                new Ordering<T>
                {
                    OrderBy = orderBy,
                    Descending = false
                }
            };
            return query;
        }

        public static Query<T> OrderByDescending<T>(this Query<T> query, Expression<Func<T, object>> orderBy)
            where T : class
        {
            query.Ordering = new List<Ordering<T>>
            {
                new Ordering<T>
                {
                    OrderBy = orderBy,
                    Descending = true
                }
            };
            return query;
        }

        public static Query<T> ThenBy<T>(this Query<T> query, Expression<Func<T, object>> thenBy)
            where T : class
        {
            query.Ordering.Add(new Ordering<T>
            {
                OrderBy = thenBy,
                Descending = false
            });
            return query;
        }

        public static Query<T> ThenByDescending<T>(this Query<T> query, Expression<Func<T, object>> thenBy)
            where T : class
        {
            query.Ordering.Add(new Ordering<T>
            {
                OrderBy = thenBy,
                Descending = true
            });
            return query;
        }
    }
}
