using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.GetByIdRange;

public class EducationCyclesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<EducationCycle, EducationCycleDto>
{
    public EducationCyclesByIdRangeRequestSpec(SearchEducationCyclesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}