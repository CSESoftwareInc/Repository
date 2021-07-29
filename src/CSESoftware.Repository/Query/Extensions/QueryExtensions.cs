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
