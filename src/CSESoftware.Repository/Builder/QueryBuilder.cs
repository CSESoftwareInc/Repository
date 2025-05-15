using CSESoftware.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace CSESoftware.Repository.Builder
{
    public class QueryBuilder<TEntity> where TEntity : class, IEntity
    {
        protected internal readonly IQuery<TEntity> Entity;
        internal List<Ordering<TEntity>> Ordering;

        public QueryBuilder()
        {
            Entity = new Query<TEntity>
            {
                Include = new List<Expression<Func<TEntity, object>>>(),
                CancellationToken = CancellationToken.None
            };

            Ordering = new List<Ordering<TEntity>>();
        }

        public QueryBuilder(IQuery<TEntity> filter)
        {
            Entity = filter;
        }

        public QueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            Entity.Predicate = expression;
            return this;
        }

        public QueryBuilder<TEntity> WhereAlso(Expression<Func<TEntity, bool>> expression)
        {
            Entity.Predicate = Entity.Predicate.AndAlso(expression);
            return this;
        }

        public QueryBuilder<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order)
        {
            Entity.OrderBy = order;
            return this;
        }

        public QueryBuilder<TEntity> OrderBy(Expression<Func<TEntity, object>> orderBy)
        {
            Ordering = new List<Ordering<TEntity>>()
            {
                new Ordering<TEntity>
                {
                    OrderBy = orderBy,
                    Descending = false
                }
            };
            return this;
        }

        public QueryBuilder<TEntity> OrderByDescending(Expression<Func<TEntity, object>> orderBy)
        {
            Ordering = new List<Ordering<TEntity>>()
            {
                new Ordering<TEntity>
                {
                    OrderBy = orderBy,
                    Descending = true
                }
            }; return this;
        }

        public QueryBuilder<TEntity> ThenBy(Expression<Func<TEntity, object>> thenBy)
        {
            Ordering.Add(new Ordering<TEntity>
            {
                OrderBy = thenBy,
                Descending = false
            });
            return this;
        }

        public QueryBuilder<TEntity> ThenByDescending(Expression<Func<TEntity, object>> thenBy)
        {
            Ordering.Add(new Ordering<TEntity>
            {
                OrderBy = thenBy,
                Descending = true
            });
            return this;
        }

        public QueryBuilder<TEntity> Include(IEnumerable<Expression<Func<TEntity, object>>> includes)
        {
            if (includes == null) return this;

            Entity.Include.AddRange(includes);
            return this;
        }

        public QueryBuilder<TEntity> Include(Expression<Func<TEntity, object>> include)
        {
            if (include == null) return this;

            Entity.Include.Add(include);
            return this;
        }

        public SelectQueryBuilder<TEntity, TOut> Select<TOut>(Expression<Func<TEntity, TOut>> select)
        {
            return new SelectQueryBuilder<TEntity, TOut>(Build(), select);
        }

        public QueryBuilder<TEntity> Skip(int? skip)
        {
            Entity.Skip = skip;
            return this;
        }

        public QueryBuilder<TEntity> Take(int? take)
        {
            Entity.Take = take;
            return this;
        }

        public QueryBuilder<TEntity> WithThisCancellationToken(CancellationToken token)
        {
            Entity.CancellationToken = token;
            return this;
        }

        public IQuery<TEntity> Build()
        {
            if (!Ordering.Any()) return Entity;

            Entity.OrderBy = x =>
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

            return Entity;
        }
    }
}
