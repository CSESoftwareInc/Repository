using System;
using System.Linq.Expressions;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository.Builder
{
    public class SelectQueryBuilder<TEntity, TOut> where TEntity : class, IEntity
    {
        internal readonly IQueryWithSelect<TEntity, TOut> _entity;

        public SelectQueryBuilder(IQuery<TEntity> entity)
        {
            _entity = new QueryWithSelect<TEntity, TOut>(entity);
        }

        public SelectQueryBuilder(IQuery<TEntity> entity, Expression<Func<TEntity, TOut>> select)
        {
            _entity = new QueryWithSelect<TEntity, TOut>(entity)
            {
                Select = select
            };
        }

        public IQueryWithSelect<TEntity, TOut> Build()
        {
            return _entity;
        }
    }
}
