using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.GetByIdRange;

public class SearchEducationPlansByIdRangeRequest : BaseFilter, IRequest<ICollection<EducationPlanDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchEducationPlansByIdRangeRequestHandler : IRequestHandler<SearchEducationPlansByIdRangeRequest, ICollection<EducationPlanDto>>
{
    private readonly IReadAudRepository<EducationPlan> _repository;

    public SearchEducationPlansByIdRangeRequestHandler(IReadAudRepository<EducationPlan> repository) => _repository = repository;

    public async Task<ICollection<EducationPlanDto>> Handle(SearchEducationPlansByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new EducationPlansByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}