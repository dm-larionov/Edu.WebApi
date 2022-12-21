using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.GetByIdRange;

public class EducationPlansByIdRangeRequestSpec : EntitiesByBaseFilterSpec<EducationPlan, EducationPlanDto>
{
    public EducationPlansByIdRangeRequestSpec(SearchEducationPlansByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}