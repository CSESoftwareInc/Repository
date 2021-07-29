﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CSESoftware.Repository
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<T>(T entity)
            where T : class;

        void Create<T>(List<T> entities)
            where T : class;

        void Update<T>(T entity)
            where T : class;

        void Update<T>(List<T> entities)
            where T : class;

        void Delete<T>(T entity)
            where T : class;

        void Delete<T>(List<T> entities)
            where T : class;

        void Delete<T>(Expression<Func<T, bool>> filter)
            where T : class;

        Task SaveAsync();
    }
}
