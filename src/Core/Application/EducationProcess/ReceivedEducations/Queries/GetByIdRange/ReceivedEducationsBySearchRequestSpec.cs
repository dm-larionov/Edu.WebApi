using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.GetByIdRange;

public class ReceivedEducationsByIdRangeRequestSpec : EntitiesByBaseFilterSpec<ReceivedEducation, ReceivedEducationDto>
{
    public ReceivedEducationsByIdRangeRequestSpec(SearchReceivedEducationsByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}