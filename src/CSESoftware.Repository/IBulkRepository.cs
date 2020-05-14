using System;
using System.Collections.Generic;
using System.Text;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IBulkRepository
    {
        void Create<TEntity>(IList<TEntity> data)
            where TEntity : class, IBaseEntity;
    }
}
