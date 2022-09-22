using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Create<TEntity>(List<TEntity> entities)
            where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Update<TEntity>(List<TEntity> entities)
            where TEntity : class, IEntity;

        void Delete<TEntity, T>(T id)
            where TEntity : class, IEntityWithId<T>;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Delete<TEntity>(List<TEntity> entities)
            where TEntity : class, IEntity;

        void Delete<TEntity>(Expression<Func<TEntity, bool>> filter)
            where TEntity : class, IEntity;

        Task SaveAsync(CancellationToken? cancellationToken = null);
    }
}
