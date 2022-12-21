using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Queries.GetByIdRange;

public class DisciplineSemestersByIdRangeRequestSpec : EntitiesByBaseFilterSpec<DisciplineSemester, DisciplineSemesterDto>
{
    public DisciplineSemestersByIdRangeRequestSpec(SearchDisciplineSemestersByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}