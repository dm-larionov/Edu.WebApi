using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Cathedras.Queries.Search;

public class SearchCathedrasRequest : PaginationFilter, IRequest<PaginationResponse<CathedraDto>>
{
}

public class SearchCathedrasRequestHandler : IRequestHandler<SearchCathedrasRequest, PaginationResponse<CathedraDto>>
{
    private readonly IReadAudRepository<Cathedra> _repository;

    public SearchCathedrasRequestHandler(IReadAudRepository<Cathedra> repository) => _repository = repository;

    public async Task<PaginationResponse<CathedraDto>> Handle(SearchCathedrasRequest request, CancellationToken cancellationToken)
    {
        var spec = new CathedrasBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}