using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.Search;

public class SearchFixedDisciplinesRequest : PaginationFilter, IRequest<PaginationResponse<FixedDisciplineDto>>
{
}

public class SearchFixedDisciplinesRequestHandler : IRequestHandler<SearchFixedDisciplinesRequest, PaginationResponse<FixedDisciplineDto>>
{
    private readonly IReadAudRepository<FixedDiscipline> _repository;

    public SearchFixedDisciplinesRequestHandler(IReadAudRepository<FixedDiscipline> repository) => _repository = repository;

    public async Task<PaginationResponse<FixedDisciplineDto>> Handle(SearchFixedDisciplinesRequest request, CancellationToken cancellationToken)
    {
        var spec = new FixedDisciplinesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}