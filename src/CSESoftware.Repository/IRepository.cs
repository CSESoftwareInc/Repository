using System.Threading.Tasks;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;

        void Update<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;

        void Delete<TEntity>(object id)
            where TEntity : class, IBaseEntity;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity;

        Task SaveAsync();
    }
}