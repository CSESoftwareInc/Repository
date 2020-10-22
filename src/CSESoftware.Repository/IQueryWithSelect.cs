using System;
using System.Linq.Expressions;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository
{
    public interface IQueryWithSelect<TEntity, TOut>  : IQuery<TEntity> where TEntity : class, IEntity
    {
        Expression<Func<TEntity, TOut>> Select { get; set; }
    }
}
