using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository.Builder
{
    public class QueryBuilder<TEntity> where TEntity : class, IEntity
    {
        protected internal readonly IQuery<TEntity> _entity;

        public QueryBuilder()
        {
            _entity = new Query<TEntity>
            {
                Include = new List<Expression<Func<TEntity, object>>>(),
                CancellationToken = CancellationToken.None
            };
        }

        public QueryBuilder(IQuery<TEntity> filter)
        {
            _entity = filter;
        }

        public QueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            _entity.Predicate = expression;
            return this;
        }

        public QueryBuilder<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order)
        {
            _entity.OrderBy = order;
            return this;
        }

        public QueryBuilder<TEntity> Include(IEnumerable<Expression<Func<TEntity, object>>> includes)
        {
            if (includes == null) return this;

            _entity.Include.AddRange(includes);
            return this;
        }

        public QueryBuilder<TEntity> Include(Expression<Func<TEntity, object>> include)
        {
            if (include == null) return this;

            _entity.Include.Add(include);
            return this;
        }

        public QueryBuilder<TEntity> Select(Expression<Func<TEntity, object>> select)
        {
            _entity.Select = select;
            return this;
        }

        public QueryBuilder<TEntity> Skip(int? skip)
        {
            _entity.Skip = skip;
            return this;
        }

        public QueryBuilder<TEntity> Take(int? take)
        {
            _entity.Take = take;
            return this;
        }

        public QueryBuilder<TEntity> WithThisCancellationTokenToken(CancellationToken token)
        {
            _entity.CancellationToken = token;
            return this;
        }

        public IQuery<TEntity> Build()
        {
            return _entity;
        }
    }
}
