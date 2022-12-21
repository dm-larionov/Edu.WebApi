using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.AudienceTypes.Queries.Search;

public class SearchAudienceTypesRequest : PaginationFilter, IRequest<PaginationResponse<AudienceTypeDto>>
{
}

public class SearchAudienceTypesRequestHandler : IRequestHandler<SearchAudienceTypesRequest, PaginationResponse<AudienceTypeDto>>
{
    private readonly IReadAudRepository<AudienceType> _repository;

    public SearchAudienceTypesRequestHandler(IReadAudRepository<AudienceType> repository) => _repository = repository;

    public async Task<PaginationResponse<AudienceTypeDto>> Handle(SearchAudienceTypesRequest request, CancellationToken cancellationToken)
    {
        var spec = new AudienceTypesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}