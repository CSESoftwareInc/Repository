using System;
using System.Collections.Generic;
using System.Text;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IBulkRepository
    {
        IEnumerable<int> Create<TEntity>(IList<TEntity> data)
            where TEntity : class, IBaseEntity;
    }
}
