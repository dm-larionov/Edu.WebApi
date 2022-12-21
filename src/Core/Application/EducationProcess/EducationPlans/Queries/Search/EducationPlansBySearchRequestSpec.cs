using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.Search;

public class EducationPlansBySearchRequestSpec : EntitiesByPaginationFilterSpec<EducationPlan, EducationPlanDto>
{
    public EducationPlansBySearchRequestSpec(SearchEducationPlansRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}