using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using CSESoftware.Core.Entity;

namespace CSESoftware.Repository.Builder
{
    public class Query<TEntity> : IQuery<TEntity> where TEntity : class
    {
        public Expression<Func<TEntity, bool>> Predicate { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set; }
        public List<Expression<Func<TEntity, object>>> Include { get; set; }
        public Expression<Func<TEntity, object>> Select { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}