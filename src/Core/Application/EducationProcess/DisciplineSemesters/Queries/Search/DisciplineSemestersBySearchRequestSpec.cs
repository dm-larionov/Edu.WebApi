using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Queries.Search;

public class DisciplineSemestersBySearchRequestSpec : EntitiesByPaginationFilterSpec<DisciplineSemester, DisciplineSemesterDto>
{
    public DisciplineSemestersBySearchRequestSpec(SearchDisciplineSemestersRequest request)
        : base(request) =>
        Query
            .Where(p => p.EducationPlans.Any(x => x.Id == request.EducationPlanId!.Value), request.EducationPlanId.HasValue);
}