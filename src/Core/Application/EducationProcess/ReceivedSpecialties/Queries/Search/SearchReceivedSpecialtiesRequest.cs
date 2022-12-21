using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.Search;

public class SearchReceivedSpecialtiesRequest : PaginationFilter, IRequest<PaginationResponse<ReceivedSpecialtyDto>>
{
}

public class SearchReceivedSpecialtiesRequestHandler : IRequestHandler<SearchReceivedSpecialtiesRequest, PaginationResponse<ReceivedSpecialtyDto>>
{
    private readonly IReadAudRepository<ReceivedSpecialty> _repository;

    public SearchReceivedSpecialtiesRequestHandler(IReadAudRepository<ReceivedSpecialty> repository) => _repository = repository;

    public async Task<PaginationResponse<ReceivedSpecialtyDto>> Handle(SearchReceivedSpecialtiesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ReceivedSpecialtiesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}