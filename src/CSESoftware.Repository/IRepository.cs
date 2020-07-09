using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Create<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Update<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntity;

        void Delete<TEntity>(object id)
            where TEntity : class, IEntityWithId;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Delete<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IEntity;

        void Delete<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class, IEntity;

        Task SaveAsync();
    }
}
