using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.Search;

public class ReceivedSpecialtiesBySearchRequestSpec : EntitiesByPaginationFilterSpec<ReceivedSpecialty, ReceivedSpecialtyDto>
{
    public ReceivedSpecialtiesBySearchRequestSpec(SearchReceivedSpecialtiesRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}