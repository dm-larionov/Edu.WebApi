using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.Search;

public class SearchEducationPlansRequest : PaginationFilter, IRequest<PaginationResponse<EducationPlanDto>>
{
}

public class SearchEducationPlansRequestHandler : IRequestHandler<SearchEducationPlansRequest, PaginationResponse<EducationPlanDto>>
{
    private readonly IReadAudRepository<EducationPlan> _repository;

    public SearchEducationPlansRequestHandler(IReadAudRepository<EducationPlan> repository) => _repository = repository;

    public async Task<PaginationResponse<EducationPlanDto>> Handle(SearchEducationPlansRequest request, CancellationToken cancellationToken)
    {
        var spec = new EducationPlansBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}