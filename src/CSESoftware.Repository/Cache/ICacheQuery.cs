using System.Runtime.Caching;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository.Cache
{
    public interface ICacheQuery<TEntity> : IQuery<TEntity> where TEntity : class, IBaseEntity
    {
        CacheItemPolicy Policy { get; set; }
    }
}