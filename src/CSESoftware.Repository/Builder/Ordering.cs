using CSESoftware.Core.Entity;
using System;
using System.Linq.Expressions;

namespace CSESoftware.Repository.Builder
{
    internal class Ordering<TEntity> where TEntity : class, IEntity
    {
        public Expression<Func<TEntity, object>> OrderBy { get; set; }
        public bool Descending { get; set; }
    }
}
