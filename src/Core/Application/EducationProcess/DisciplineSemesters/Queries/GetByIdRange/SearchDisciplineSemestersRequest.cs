using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Queries.GetByIdRange;

public class SearchDisciplineSemestersByIdRangeRequest : BaseFilter, IRequest<ICollection<DisciplineSemesterDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchDisciplineSemestersByIdRangeRequestHandler : IRequestHandler<SearchDisciplineSemestersByIdRangeRequest, ICollection<DisciplineSemesterDto>>
{
    private readonly IReadAudRepository<DisciplineSemester> _repository;

    public SearchDisciplineSemestersByIdRangeRequestHandler(IReadAudRepository<DisciplineSemester> repository) => _repository = repository;

    public async Task<ICollection<DisciplineSemesterDto>> Handle(SearchDisciplineSemestersByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new DisciplineSemestersByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}