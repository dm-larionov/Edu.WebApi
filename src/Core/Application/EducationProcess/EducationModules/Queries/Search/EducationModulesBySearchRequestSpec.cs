using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationModules.Queries.Search;

public class EducationModulesBySearchRequestSpec : EntitiesByPaginationFilterSpec<EducationModule, EducationModuleDto>
{
    public EducationModulesBySearchRequestSpec(SearchEducationModulesRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}