using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Caching;
using System.Threading.Tasks;
using CSESoftware.Core.Entity;
using CSESoftware.Repository.Exceptions;

namespace CSESoftware.Repository.Cache
{
    public class CachedRepository : ICachedRepository
    {
        private readonly IReadOnlyRepository _readOnlyRepository;
        private static readonly MemoryCache Cache = MemoryCache.Default;
        private static readonly object CacheThreadLock = new object();

        public CachedRepository(IReadOnlyRepository readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }

        /// <summary>
        /// Gets all entities with preference of using the cache
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAll<TEntity>(ICacheQuery<TEntity> filter)
            where TEntity : class, IBaseEntity
        {
            if(filter.Policy == null)
                throw new CachePolicyNullException();

            var includeProps = (filter.Include ?? new List<Expression<Func<TEntity, object>>>()).ToList();
            var key = typeof(TEntity) + FilterToString(filter.Predicate) + IncludePropertiesToString(includeProps) + "skip" + filter.Skip + "take" + filter.Take + "GetAll";

            // Check the cache. If what you are searching for exists, return it
            var cachedResult = GetCachedItem(key);
            if (cachedResult != null) return (IEnumerable<TEntity>)cachedResult; //todo - error handling on all casts in case of cache collision

            // Didn't exist in cache. Get from database and add to cache.
            var result = await _readOnlyRepository.GetAllAsync(filter);
            Add(key, result, filter.Policy);
            return result;
        }

        /// <summary>
        /// Gets single entity with preference of using the cache
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<TEntity> GetFirst<TEntity>(ICacheQuery<TEntity> filter)
            where TEntity : class, IBaseEntity
        {
            if (filter.Policy == null)
                throw new CachePolicyNullException();

            var includeProps = (filter.Include ?? new List<Expression<Func<TEntity, object>>>()).ToList();
            var key = typeof(TEntity) + FilterToString(filter.Predicate) + IncludePropertiesToString(includeProps) + "GetOne";

            // Check the cache. If what you are searching for exists, return it
            var cachedResult = GetCachedItem(key);
            if (cachedResult != null) return (TEntity)cachedResult;

            // Didn't exist in cache. Get from database and add to cache.
            var result = await _readOnlyRepository.GetFirstAsync(filter);
            Add(key, result, filter.Policy);
            return result;
        }

        /// <summary>
        /// Gets the count of the entities the filter would return with preference of using the cache
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="policy"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<int> GetCount<TEntity>(
            CacheItemPolicy policy,
            Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IBaseEntity
        {
            var key = typeof(TEntity) + FilterToString(filter) + "GetCount";

            // Check the cache. If what you are searching for exists, return it
            var cachedResult = GetCachedItem(key);
            if (cachedResult != null) return (int)cachedResult;

            // Didn't exist in cache. Get from database and add to cache.
            var result = await _readOnlyRepository.GetCountAsync(filter);
            Add(key, result, policy);
            return result;
        }

        /// <summary>
        /// Gets whether the entity exists with preference of using the cache
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="policy"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<bool> GetExists<TEntity>(
            CacheItemPolicy policy,
            Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IBaseEntity
        {
            var key = typeof(TEntity) + FilterToString(filter) + "GetExists";

            // Check the cache. If what you are searching for exists, return it
            var cachedResult = GetCachedItem(key);
            if (cachedResult != null) return (bool)cachedResult;

            // Didn't exist in cache. Get from database and add to cache.
            var result = await _readOnlyRepository.GetExistsAsync(filter);
            Add(key, result, policy);
            return result;
        }

        /// <summary>
        /// Get a cache policy with an absolute expiration of the defined seconds
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public CacheItemPolicy GetPolicy(int seconds)
        {
            return new CacheItemPolicy()
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddSeconds(seconds))
            };
        }

        /// <summary>
        /// Clears the entire cache
        /// </summary>
        /// <returns></returns>
        public void ClearCache()
        {
            lock (CacheThreadLock)
            {
                foreach (var key in Cache.Select(k => k.Key))
                {
                    Cache.Remove(key);
                }
            }
        }

        /// <summary>
        /// Removes the given key from the cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void ClearCache(string key)
        {
            lock (CacheThreadLock)
            {
                Cache.Remove(key);
            }
        }


        private void Add(string key, object data, CacheItemPolicy policy)
        {
            lock (CacheThreadLock)
            {
                Cache.Set(key, data, policy);
            }
        }


        private object GetCachedItem(string key)
        {
            lock (CacheThreadLock)
            {
                return Cache.Get(key);
            }
        }

        private static string FilterToString(LambdaExpression func)
        {
            if (func == null) return "";

            var expBody = func.Body.ToString();

            var paramName = func.Parameters[0].Name;
            var paramTypeName = func.Parameters[0].Type.Name;

            return expBody.Replace(paramName + ".", paramTypeName + ".")
                .Replace("AndAlso", "&&");
        }

        private static string IncludePropertiesToString<TEntity>(IEnumerable<Expression<Func<TEntity, object>>> includeProperties)
            where TEntity : class, IBaseEntity
        {
            if (includeProperties == null)
                return "";

            var properties = "";

            foreach (var property in includeProperties)
            {
                properties += FilterToString(property);
            }

            return properties;
        }
    }
}