using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.GetByIdRange;

public class ReceivedEducationFormsByIdRangeRequestSpec : EntitiesByBaseFilterSpec<ReceivedEducationForm, ReceivedEducationFormDto>
{
    public ReceivedEducationFormsByIdRangeRequestSpec(SearchReceivedEducationFormsByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}