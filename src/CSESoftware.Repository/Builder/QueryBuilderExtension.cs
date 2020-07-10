using CSESoftware.Core.Entity;

namespace CSESoftware.Repository.Builder
{
    public static class QueryBuilderExtension
    {
        public static QueryBuilder<TEntity> WithId<TEntity>(this QueryBuilder<TEntity> queryBuilder,  object id)
            where TEntity : class, IEntityWithId
        {
            queryBuilder._entity.Predicate = x => x.Id.Equals(id);
            return queryBuilder;
        }
    }
}
