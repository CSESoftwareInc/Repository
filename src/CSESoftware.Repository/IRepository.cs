using CSESoftware.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CSESoftware.Repository
{
    public interface IRepository : IReadOnlyRepository
    {
        Task CreateAsync<T>(T entity)
            where T : class;

        Task CreateAsync<T>(List<T> entities)
            where T : class;

        Task UpdateAsync<T>(T entity)
            where T : class;

        Task UpdateAsync<T>(List<T> entities)
            where T : class;

        Task DeleteAsync<T, TId>(TId id)
            where T : class, IEntityWithId<TId>;

        Task DeleteAsync<T>(T entity)
            where T : class;

        Task DeleteAsync<T>(List<T> entities)
            where T : class;

        Task DeleteAsync<T>(Expression<Func<T, bool>> filter)
            where T : class;
    }
}
