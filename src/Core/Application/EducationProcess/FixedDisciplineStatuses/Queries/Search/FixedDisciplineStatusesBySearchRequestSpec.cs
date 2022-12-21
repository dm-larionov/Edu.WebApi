using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.Search;

public class FixedDisciplineStatusesBySearchRequestSpec : EntitiesByPaginationFilterSpec<FixedDisciplineStatus, FixedDisciplineStatusDto>
{
    public FixedDisciplineStatusesBySearchRequestSpec(SearchFixedDisciplineStatusesRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}