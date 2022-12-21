using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.AudienceTypes.Queries.Search;

public class AudienceTypesBySearchRequestSpec : EntitiesByPaginationFilterSpec<AudienceType, AudienceTypeDto>
{
    public AudienceTypesBySearchRequestSpec(SearchAudienceTypesRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}