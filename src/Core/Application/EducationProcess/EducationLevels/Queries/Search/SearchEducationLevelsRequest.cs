using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationLevels.Queries.Search;

public class SearchEducationLevelsRequest : PaginationFilter, IRequest<PaginationResponse<EducationLevelDto>>
{
}

public class SearchEducationLevelsRequestHandler : IRequestHandler<SearchEducationLevelsRequest, PaginationResponse<EducationLevelDto>>
{
    private readonly IReadAudRepository<EducationLevel> _repository;

    public SearchEducationLevelsRequestHandler(IReadAudRepository<EducationLevel> repository) => _repository = repository;

    public async Task<PaginationResponse<EducationLevelDto>> Handle(SearchEducationLevelsRequest request, CancellationToken cancellationToken)
    {
        var spec = new EducationLevelsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}