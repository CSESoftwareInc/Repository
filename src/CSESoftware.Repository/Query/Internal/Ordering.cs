using System;
using System.Linq.Expressions;

namespace CSESoftware.Repository.Query.Internal
{
    internal class Ordering<T> where T : class
    {
        public Expression<Func<T, object>> OrderBy { get; set; }
        public bool Descending { get; set; }
    }
}
