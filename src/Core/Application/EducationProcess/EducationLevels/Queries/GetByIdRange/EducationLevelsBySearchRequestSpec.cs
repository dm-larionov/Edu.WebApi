using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationLevels.Queries.GetByIdRange;

public class EducationLevelsByIdRangeRequestSpec : EntitiesByBaseFilterSpec<EducationLevel, EducationLevelDto>
{
    public EducationLevelsByIdRangeRequestSpec(SearchEducationLevelsByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}