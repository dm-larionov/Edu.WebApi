using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.Search;

public class DisciplineSchedulesBySearchRequestSpec : EntitiesByPaginationFilterSpec<DisciplineSchedule, DisciplineScheduleDto>
{
    public DisciplineSchedulesBySearchRequestSpec(SearchDisciplineSchedulesRequest request)
        : base(request) =>
        Query
            .Where(p => p.FixedDisciplineId == request.FixedDisciplineId, request.FixedDisciplineId.HasValue);
}