using CSESoftware.Core.Entity;
using System;
using System.Linq.Expressions;

namespace CSESoftware.Repository.Builder
{
    public class SelectQueryBuilder<TEntity, TOut> where TEntity : class, IEntity
    {
        protected internal readonly IQueryWithSelect<TEntity, TOut> Entity;

        public SelectQueryBuilder(IQuery<TEntity> entity)
        {
            Entity = new QueryWithSelect<TEntity, TOut>(entity);
        }

        public SelectQueryBuilder(IQuery<TEntity> entity, Expression<Func<TEntity, TOut>> select)
        {
            Entity = new QueryWithSelect<TEntity, TOut>(entity)
            {
                Select = select
            };
        }

        public SelectQueryBuilder<TEntity, TOut> Distinct()
        {
            Entity.Distinct = true;
            return this;
        }

        public IQueryWithSelect<TEntity, TOut> Build()
        {
            return Entity;
        }
    }
}
