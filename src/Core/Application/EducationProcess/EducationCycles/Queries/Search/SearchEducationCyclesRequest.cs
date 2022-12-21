using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.Search;

public class SearchEducationCyclesRequest : PaginationFilter, IRequest<PaginationResponse<EducationCycleDto>>
{
}

public class SearchEducationCyclesRequestHandler : IRequestHandler<SearchEducationCyclesRequest, PaginationResponse<EducationCycleDto>>
{
    private readonly IReadAudRepository<EducationCycle> _repository;

    public SearchEducationCyclesRequestHandler(IReadAudRepository<EducationCycle> repository) => _repository = repository;

    public async Task<PaginationResponse<EducationCycleDto>> Handle(SearchEducationCyclesRequest request, CancellationToken cancellationToken)
    {
        var spec = new EducationCyclesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}