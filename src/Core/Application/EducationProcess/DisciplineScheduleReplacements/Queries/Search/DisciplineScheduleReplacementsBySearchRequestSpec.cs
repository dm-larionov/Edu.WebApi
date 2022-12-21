using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.Search;

public class DisciplineScheduleReplacementsBySearchRequestSpec : EntitiesByPaginationFilterSpec<DisciplineScheduleReplacement, DisciplineScheduleReplacementDto>
{
    public DisciplineScheduleReplacementsBySearchRequestSpec(SearchDisciplineScheduleReplacementsRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}