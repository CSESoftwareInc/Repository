using CSESoftware.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CSESoftware.Repository
{
    public interface IReadOnlyRepository
    {
        Task<List<TEntity>> GetAllAsync<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IEntity;

        List<TEntity> GetAll<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IEntity;

        Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        List<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<TEntity> GetFirstAsync<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IEntity;

        TEntity GetFirst<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IEntity;

        Task<TEntity> GetFirstAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        TEntity GetFirst<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<int> GetCountAsync<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IEntity;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<bool> GetExistsAsync<TEntity>(IQuery<TEntity> filter)
            where TEntity : class, IEntity;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        Task<List<TOut>> GetAllWithSelectAsync<TEntity, TOut>(IQueryWithSelect<TEntity, TOut> filter = null)
            where TEntity : class, IEntity;

        List<TOut> GetAllWithSelect<TEntity, TOut>(IQueryWithSelect<TEntity, TOut> filter = null)
            where TEntity : class, IEntity;

        Task<TOut> GetFirstWithSelectAsync<TEntity, TOut>(IQueryWithSelect<TEntity, TOut> filter = null)
            where TEntity : class, IEntity;

        TOut GetFirstWithSelect<TEntity, TOut>(IQueryWithSelect<TEntity, TOut> filter = null)
            where TEntity : class, IEntity;

        Task<int> GetCountWithSelectAsync<TEntity, TOut>(IQueryWithSelect<TEntity, TOut> filter = null)
            where TEntity : class, IEntity;
    }
}
