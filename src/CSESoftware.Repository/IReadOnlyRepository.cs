using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IReadOnlyRepository {

        Task<List<TEntity>> GetAllAsync<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IEntity;

        Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<TEntity> GetFirstAsync<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IEntity;

        Task<TEntity> GetFirstAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<int> GetCountAsync<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IEntity;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<bool> GetExistsAsync<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IEntity;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<List<TOut>> GetAllWithSelectAsync<TEntity, TOut>(IQuery<TEntity> filter = null)
            where TEntity : class, IEntity;
    }
}
