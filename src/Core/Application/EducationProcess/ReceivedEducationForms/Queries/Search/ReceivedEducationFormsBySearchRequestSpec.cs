using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.Search;

public class ReceivedEducationFormsBySearchRequestSpec : EntitiesByPaginationFilterSpec<ReceivedEducationForm, ReceivedEducationFormDto>
{
    public ReceivedEducationFormsBySearchRequestSpec(SearchReceivedEducationFormsRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}