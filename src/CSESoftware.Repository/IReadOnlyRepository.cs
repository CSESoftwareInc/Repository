using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IReadOnlyRepository {

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IBaseEntity;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IBaseEntity;

        Task<TEntity> GetFirstAsync<TEntity>(IQuery<TEntity> filter = null)
            where TEntity : class, IBaseEntity;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IBaseEntity;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IBaseEntity;

        Task<IEnumerable<object>> GetAllWithSelectAsync<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IBaseEntity;
    }
}