using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationModules.Queries.GetByIdRange;

public class EducationModulesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<EducationModule, EducationModuleDto>
{
    public EducationModulesByIdRangeRequestSpec(SearchEducationModulesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}