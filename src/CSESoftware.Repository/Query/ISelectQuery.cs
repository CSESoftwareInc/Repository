using System;
using System.Linq.Expressions;

namespace CSESoftware.Repository.Query
{
    public interface ISelectQuery<T, TOut> : IQuery<T> where T : class
    {
        Expression<Func<T, TOut>> Select { get; set; }
    }
}
