using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetByIdRange;

public class DisciplineSchedulesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<DisciplineSchedule, DisciplineScheduleDto>
{
    public DisciplineSchedulesByIdRangeRequestSpec(SearchDisciplineSchedulesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}