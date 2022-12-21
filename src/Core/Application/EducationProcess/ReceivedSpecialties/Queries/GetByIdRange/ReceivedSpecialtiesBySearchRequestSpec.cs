using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.GetByIdRange;

public class ReceivedSpecialtiesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<ReceivedSpecialty, ReceivedSpecialtyDto>
{
    public ReceivedSpecialtiesByIdRangeRequestSpec(SearchReceivedSpecialtiesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}