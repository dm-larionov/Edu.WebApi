using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Audiences.Queries.Search;

public class SearchAudiencesRequest : PaginationFilter, IRequest<PaginationResponse<AudienceDto>>
{
}

public class SearchAudiencesRequestHandler : IRequestHandler<SearchAudiencesRequest, PaginationResponse<AudienceDto>>
{
    private readonly IReadAudRepository<Audience> _repository;

    public SearchAudiencesRequestHandler(IReadAudRepository<Audience> repository) => _repository = repository;

    public async Task<PaginationResponse<AudienceDto>> Handle(SearchAudiencesRequest request, CancellationToken cancellationToken)
    {
        var spec = new AudiencesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}