using CSESoftware.Core.Entity;
using System;
using System.Linq.Expressions;

namespace CSESoftware.Repository
{
    public interface IQueryWithSelect<TEntity, TOut> : IQuery<TEntity> where TEntity : class, IEntity
    {
        Expression<Func<TEntity, TOut>> Select { get; set; }
        bool Distinct { get; set; }
    }
}
