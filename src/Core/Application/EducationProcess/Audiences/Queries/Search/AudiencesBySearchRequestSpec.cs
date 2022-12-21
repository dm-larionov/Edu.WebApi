using Edu.WebApi.Application.EducationProcess.Audiences.Queries.GetById;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Audiences.Queries.Search;

public class AudiencesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Audience, AudienceDto>
{
    public AudiencesBySearchRequestSpec(SearchAudiencesRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}