using CSESoftware.Core.Entity;

namespace CSESoftware.Repository.Builder
{
    public static class QueryBuilderExtension
    {
        public static QueryBuilder<TEntity> WithId<TEntity, T>(this QueryBuilder<TEntity> queryBuilder,  T id)
            where TEntity : class, IEntityWithId<T>
        {
            queryBuilder._entity.Predicate = x => x.Id.Equals(id);
            return queryBuilder;
        }
    }
}
