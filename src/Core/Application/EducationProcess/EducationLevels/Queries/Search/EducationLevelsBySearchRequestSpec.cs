using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationLevels.Queries.Search;

public class EducationLevelsBySearchRequestSpec : EntitiesByPaginationFilterSpec<EducationLevel, EducationLevelDto>
{
    public EducationLevelsBySearchRequestSpec(SearchEducationLevelsRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}