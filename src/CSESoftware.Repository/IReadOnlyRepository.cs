using CSESoftware.Repository.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CSESoftware.Repository
{
    public interface IReadOnlyRepository {
        Task<List<T>> GetAllAsync<T>(IQuery<T> filter)
            where T : class;

        Task<List<T>> GetAllAsync<T>(Expression<Func<T, bool>> filter = null)
            where T : class;

        Task<T> GetFirstAsync<T>(IQuery<T> filter)
            where T : class;

        Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> filter = null)
            where T : class;

        Task<int> GetCountAsync<T>(IQuery<T> filter)
            where T : class;

        Task<int> GetCountAsync<T>(Expression<Func<T, bool>> filter = null)
            where T : class;

        Task<bool> GetExistsAsync<T>(IQuery<T> filter)
            where T : class;

        Task<bool> GetExistsAsync<T>(Expression<Func<T, bool>> filter = null)
            where T : class;

        Task<List<TOut>> GetAllWithSelectAsync<T, TOut>(ISelectQuery<T, TOut> filter = null)
            where T : class;

        Task<TOut> GetFirstWithSelectAsync<T, TOut>(ISelectQuery<T, TOut> filter = null)
            where T : class;
    }
}
