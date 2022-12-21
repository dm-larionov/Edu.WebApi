using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Audiences.Queries.GetByIdRange;

public class AudiencesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<Audience, AudienceDto>
{
    public AudiencesByIdRangeRequestSpec(SearchAudiencesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}