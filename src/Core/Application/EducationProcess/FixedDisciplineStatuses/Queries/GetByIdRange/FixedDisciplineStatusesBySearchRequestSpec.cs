using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.GetByIdRange;

public class FixedDisciplineStatusesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<FixedDisciplineStatus, FixedDisciplineStatusDto>
{
    public FixedDisciplineStatusesByIdRangeRequestSpec(SearchFixedDisciplineStatusesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}