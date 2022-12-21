using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.Search;

public class SearchReceivedEducationFormsRequest : PaginationFilter, IRequest<PaginationResponse<ReceivedEducationFormDto>>
{
}

public class SearchReceivedEducationFormsRequestHandler : IRequestHandler<SearchReceivedEducationFormsRequest, PaginationResponse<ReceivedEducationFormDto>>
{
    private readonly IReadAudRepository<ReceivedEducationForm> _repository;

    public SearchReceivedEducationFormsRequestHandler(IReadAudRepository<ReceivedEducationForm> repository) => _repository = repository;

    public async Task<PaginationResponse<ReceivedEducationFormDto>> Handle(SearchReceivedEducationFormsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ReceivedEducationFormsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}