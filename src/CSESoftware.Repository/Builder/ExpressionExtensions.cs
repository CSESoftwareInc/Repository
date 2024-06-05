using System;
using System.Linq;
using System.Linq.Expressions;

namespace CSESoftware.Repository.Builder
{
    internal static class ExpressionExtensions
    {
        internal static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T), "entity");

            var leftVisitor = new ReplaceExpressionVisitor(left.Parameters.First(), parameter);
            var leftBody = leftVisitor.Visit(left.Body);

            var rightVisitor = new ReplaceExpressionVisitor(right.Parameters.First(), parameter);
            var rightBody = rightVisitor.Visit(right.Body);

            var body = Expression.AndAlso(leftBody!, rightBody!);

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private class ReplaceExpressionVisitor(Expression oldValue, Expression newValue) : ExpressionVisitor
        {
            public override Expression Visit(Expression expression)
            {
                return expression == oldValue ? newValue : base.Visit(expression);
            }
        }
    }
}
