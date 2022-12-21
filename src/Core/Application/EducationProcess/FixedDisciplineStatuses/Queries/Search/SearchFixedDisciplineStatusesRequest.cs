using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.Search;

public class SearchFixedDisciplineStatusesRequest : PaginationFilter, IRequest<PaginationResponse<FixedDisciplineStatusDto>>
{
}

public class SearchFixedDisciplineStatusesRequestHandler : IRequestHandler<SearchFixedDisciplineStatusesRequest, PaginationResponse<FixedDisciplineStatusDto>>
{
    private readonly IReadAudRepository<FixedDisciplineStatus> _repository;

    public SearchFixedDisciplineStatusesRequestHandler(IReadAudRepository<FixedDisciplineStatus> repository) => _repository = repository;

    public async Task<PaginationResponse<FixedDisciplineStatusDto>> Handle(SearchFixedDisciplineStatusesRequest request, CancellationToken cancellationToken)
    {
        var spec = new FixedDisciplineStatusesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}