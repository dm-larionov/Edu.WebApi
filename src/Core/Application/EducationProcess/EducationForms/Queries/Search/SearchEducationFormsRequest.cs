using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationForms.Queries.Search;

public class SearchEducationFormsRequest : PaginationFilter, IRequest<PaginationResponse<EducationFormDto>>
{
}

public class SearchEducationFormsRequestHandler : IRequestHandler<SearchEducationFormsRequest, PaginationResponse<EducationFormDto>>
{
    private readonly IReadAudRepository<EducationForm> _repository;

    public SearchEducationFormsRequestHandler(IReadAudRepository<EducationForm> repository) => _repository = repository;

    public async Task<PaginationResponse<EducationFormDto>> Handle(SearchEducationFormsRequest request, CancellationToken cancellationToken)
    {
        var spec = new EducationFormsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}