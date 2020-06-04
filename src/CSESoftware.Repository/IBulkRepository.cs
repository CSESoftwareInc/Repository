using System.Collections.Generic;
using System.Threading.Tasks;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IBulkRepository : IRepository
    {
        Task<IEnumerable<object>> CreateAsync<TEntity>(IEnumerable<TEntity> data) where TEntity : class, IBaseEntity;
        Task DeleteAsync<TEntity>(IEnumerable<object> ids) where TEntity : class, IBaseEntity;
        Task<IEnumerable<TEntity>> SelectAsync<TEntity>(IEnumerable<object> ids) where TEntity : class, IBaseEntity;
    }
}