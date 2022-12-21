using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Queries.Search;

public class SearchDisciplineSemestersRequest : PaginationFilter, IRequest<PaginationResponse<DisciplineSemesterDto>>
{
    public int? EducationPlanId { get; set; }
}

public class SearchDisciplineSemestersRequestHandler : IRequestHandler<SearchDisciplineSemestersRequest, PaginationResponse<DisciplineSemesterDto>>
{
    private readonly IReadAudRepository<DisciplineSemester> _repository;

    public SearchDisciplineSemestersRequestHandler(IReadAudRepository<DisciplineSemester> repository) => _repository = repository;

    public async Task<PaginationResponse<DisciplineSemesterDto>> Handle(SearchDisciplineSemestersRequest request, CancellationToken cancellationToken)
    {
        var spec = new DisciplineSemestersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}