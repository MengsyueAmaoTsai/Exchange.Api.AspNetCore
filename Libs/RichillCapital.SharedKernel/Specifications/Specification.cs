using System.Linq.Expressions;

namespace RichillCapital.SharedKernel.Specifications;

public abstract class Specification<T, TProperty>
{
}

public abstract class Specification<T>
{
    protected Specification()
    {
        Query = new SpecificationBuilder<T>(this);
    }

    public virtual SpecificationBuilder<T> Query { get; private init; }

    public IEnumerable<Expression<Func<T, object>>> IncludeExpressions { get; } = new List<Expression<Func<T, object>>>();

    public IEnumerable<Expression<Func<T, bool>>> WhereExpressions { get; } = new List<Expression<Func<T, bool>>>();

    public IEnumerable<Expression<Func<T, object>>> OrderExpressions { get; } = new List<Expression<Func<T, object>>>();
}