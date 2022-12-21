using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.Search;

public class EducationCyclesBySearchRequestSpec : EntitiesByPaginationFilterSpec<EducationCycle, EducationCycleDto>
{
    public EducationCyclesBySearchRequestSpec(SearchEducationCyclesRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}