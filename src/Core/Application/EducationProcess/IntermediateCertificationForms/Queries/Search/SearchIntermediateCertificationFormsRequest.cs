using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Queries.Search;

public class SearchIntermediateCertificationFormsRequest : PaginationFilter, IRequest<PaginationResponse<IntermediateCertificationFormDto>>
{
}

public class SearchIntermediateCertificationFormsRequestHandler : IRequestHandler<SearchIntermediateCertificationFormsRequest, PaginationResponse<IntermediateCertificationFormDto>>
{
    private readonly IReadAudRepository<IntermediateCertificationForm> _repository;

    public SearchIntermediateCertificationFormsRequestHandler(IReadAudRepository<IntermediateCertificationForm> repository) => _repository = repository;

    public async Task<PaginationResponse<IntermediateCertificationFormDto>> Handle(SearchIntermediateCertificationFormsRequest request, CancellationToken cancellationToken)
    {
        var spec = new IntermediateCertificationFormsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}