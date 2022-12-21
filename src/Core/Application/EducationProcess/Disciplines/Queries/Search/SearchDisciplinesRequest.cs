using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Disciplines.Queries.Search;

public class SearchDisciplinesRequest : PaginationFilter, IRequest<PaginationResponse<DisciplineDto>>
{
}

public class SearchDisciplinesRequestHandler : IRequestHandler<SearchDisciplinesRequest, PaginationResponse<DisciplineDto>>
{
    private readonly IReadAudRepository<Discipline> _repository;

    public SearchDisciplinesRequestHandler(IReadAudRepository<Discipline> repository) => _repository = repository;

    public async Task<PaginationResponse<DisciplineDto>> Handle(SearchDisciplinesRequest request, CancellationToken cancellationToken)
    {
        var spec = new DisciplinesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}