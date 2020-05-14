using System;
using System.Collections.Generic;
using System.Text;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IBulkRepository
    {
        IEnumerable<object> Create<TEntity>(IEnumerable<TEntity> data)
            where TEntity : class, IBaseEntity;
    }
}
