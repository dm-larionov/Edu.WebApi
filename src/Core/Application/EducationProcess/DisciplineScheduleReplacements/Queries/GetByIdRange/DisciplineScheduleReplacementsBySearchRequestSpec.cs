using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.GetByIdRange;

public class DisciplineScheduleReplacementsByIdRangeRequestSpec : EntitiesByBaseFilterSpec<DisciplineScheduleReplacement, DisciplineScheduleReplacementDto>
{
    public DisciplineScheduleReplacementsByIdRangeRequestSpec(SearchDisciplineScheduleReplacementsByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}