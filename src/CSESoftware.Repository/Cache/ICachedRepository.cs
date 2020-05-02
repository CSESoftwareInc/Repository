using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Caching;
using System.Threading.Tasks;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository.Cache
{
    public interface ICachedRepository
    {
        Task<IEnumerable<TEntity>> GetAll<TEntity>(ICacheQuery<TEntity> filter)
            where TEntity : class, IBaseEntity;

        Task<TEntity> GetFirst<TEntity>(ICacheQuery<TEntity> filter)
            where TEntity : class, IBaseEntity;

        Task<int> GetCount<TEntity>(CacheItemPolicy policy, Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IBaseEntity;

        Task<bool> GetExists<TEntity>(CacheItemPolicy policy, Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IBaseEntity;

        CacheItemPolicy GetPolicy(int seconds);

        void ClearCache();

        void ClearCache(string key);
    }
}