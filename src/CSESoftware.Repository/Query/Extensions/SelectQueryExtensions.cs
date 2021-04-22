using System;
using System.Linq.Expressions;

namespace CSESoftware.Repository.Query.Extensions
{
    public static class SelectQueryExtensions
    {
        public static ISelectQuery<T, TOut> Select<T, TOut>(this IQuery<T> query, Expression<Func<T, TOut>> select)
            where T : class
        {
            var selectQuery = new SelectQuery<T, TOut>(query)
            {
                Select = select
            };

            return selectQuery;
        }
    }
}
