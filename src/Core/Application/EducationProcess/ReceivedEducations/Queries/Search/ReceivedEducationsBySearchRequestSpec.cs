using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.Search;

public class ReceivedEducationsBySearchRequestSpec : EntitiesByPaginationFilterSpec<ReceivedEducation, ReceivedEducationDto>
{
    public ReceivedEducationsBySearchRequestSpec(SearchReceivedEducationsRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}