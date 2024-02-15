using System.Linq.Expressions;

namespace RichillCapital.SharedKernel.Specifications;

public class SpecificationBuilder<T>
{
    public SpecificationBuilder(Specification<T> specification)
    {
        Specification = specification;
    }

    public Specification<T> Specification { get; private init; }

    public SpecificationBuilder<T> Include(Expression<Func<T, object>> includeExpression)
    {
        ((List<Expression<Func<T, object>>>)Specification.IncludeExpressions).Add(includeExpression);

        return this;
    }

    public SpecificationBuilder<T> Where(Expression<Func<T, bool>> whereExpression)
    {
        ((List<Expression<Func<T, bool>>>)Specification.WhereExpressions).Add(whereExpression);

        return this;
    }

    public SpecificationBuilder<T> OrderBy(Expression<Func<T, object>> orderExpression)
    {
        ((List<Expression<Func<T, object>>>)Specification.WhereExpressions).Add(orderExpression);

        return this;
    }
}