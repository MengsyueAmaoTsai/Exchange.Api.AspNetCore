using RichillCapital.SharedKernel.Specifications;
using RichillCapital.SharedKernel.Specifications.Evaluators;
using RichillCapital.SharedKernel.Specifications.Exceptions;

namespace RichillCapital.Persistence;

public class SpecificationEvaluator : ISpecificationEvaluator
{
    public static SpecificationEvaluator Default { get; } = new SpecificationEvaluator();

    protected List<IEvaluator> Evaluators { get; } = [];

    public SpecificationEvaluator()
    {
        Evaluators.AddRange(
        [
            WhereEvaluator.Instance,
            IncludeEvaluator.Default,
            OrderEvaluator.Instance,
            PaginationEvaluator.Instance,
            AsNoTrackingEvaluator.Instance,
            AsNoTrackingWithIdentityResolutionEvaluator.Instance,
            AsTrackingEvaluator.Instance,
            IgnoreQueryFiltersEvaluator.Instance,
            AsSplitQueryEvaluator.Instance
        ]);
    }
    public SpecificationEvaluator(IEnumerable<IEvaluator> evaluators) =>
        Evaluators.AddRange(evaluators);

    public virtual IQueryable<TResult> GetQuery<T, TResult>(
        IQueryable<T> query,
        ISpecification<T, TResult> specification)
        where T : class
    {
        if (specification is null)
        {
            throw new ArgumentNullException("Specification is required");
        }

        if (specification.Selector is null && specification.SelectorMany is null)
        {
            throw new SelectorNotFoundException();
        }

        if (specification.Selector != null && specification.SelectorMany != null)
        {
            throw new ConcurrentSelectorsException();
        }

        query = GetQuery(query, (ISpecification<T>)specification);

        return specification.Selector != null ?
            query.Select(specification.Selector) :
            query.SelectMany(specification.SelectorMany!);
    }

    public virtual IQueryable<T> GetQuery<T>(
        IQueryable<T> query,
        ISpecification<T> specification,
        bool evaluateCriteriaOnly = false)
        where T : class
    {
        if (specification is null)
        {
            throw new ArgumentNullException("Specification is required");
        }

        var evaluators = evaluateCriteriaOnly ?
            Evaluators.Where(evaluator => evaluator.IsCriteriaEvaluator) :
            Evaluators;

        foreach (var evaluator in evaluators)
        {
            query = evaluator.GetQuery(query, specification);
        }

        return query;
    }
}
