using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.AudienceTypes.Queries.GetByIdRange;

public class AudienceTypesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<AudienceType, AudienceTypeDto>
{
    public AudienceTypesByIdRangeRequestSpec(SearchAudienceTypesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}