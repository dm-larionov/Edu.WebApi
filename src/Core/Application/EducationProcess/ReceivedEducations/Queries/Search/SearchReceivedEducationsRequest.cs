using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.Search;

public class SearchReceivedEducationsRequest : PaginationFilter, IRequest<PaginationResponse<ReceivedEducationDto>>
{
}

public class SearchReceivedEducationsRequestHandler : IRequestHandler<SearchReceivedEducationsRequest, PaginationResponse<ReceivedEducationDto>>
{
    private readonly IReadAudRepository<ReceivedEducation> _repository;

    public SearchReceivedEducationsRequestHandler(IReadAudRepository<ReceivedEducation> repository) => _repository = repository;

    public async Task<PaginationResponse<ReceivedEducationDto>> Handle(SearchReceivedEducationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ReceivedEducationsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}