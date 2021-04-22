using CSESoftware.Core.Entity;
using CSESoftware.Repository.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace CSESoftware.Repository.Query.Extensions
{
    public static class QueryExtensions
    {
        public static IQuery<T> Where<T>(this IQuery<T> query, Expression<Func<T, bool>> expression)
            where T : class
        {
            query.Predicate = expression;
            return query;
        }

        public static IQuery<T> WithId<T, TId>(this IQuery<T> query, TId id)
            where T : class, IEntityWithId<TId>
        {
            query.Predicate = x => x.Id.Equals(id);
            return query;
        }

        public static IQuery<T> OrderBy<T>(this IQuery<T> query, Func<IQueryable<T>, IOrderedQueryable<T>> order)
            where T : class
        {
            query.OrderBy = order;
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

        public static IQuery<T> Include<T>(this IQuery<T> query, IEnumerable<Expression<Func<T, object>>> includes)
            where T : class
        {
            query.Include.AddRange(includes.Where(x => x != null));
            return query;
        }

        public static IQuery<T> Include<T>(this IQuery<T> query, Expression<Func<T, object>> include)
            where T : class
        {
            if (include == null) return query;

            query.Include.Add(include);
            return query;
        }

        public static IQuery<T> Skip<T>(this IQuery<T> query, int? skip)
            where T : class
        {
            query.Skip = skip;
            return query;
        }

        public static IQuery<T> Take<T>(this IQuery<T> query, int? take)
            where T : class
        {
            query.Take = take;
            return query;
        }

        public static IQuery<T> WithThisCancellationToken<T>(this IQuery<T> query, CancellationToken token)
            where T : class
        {
            query.CancellationToken = token;
            return query;
        }
    }
}
