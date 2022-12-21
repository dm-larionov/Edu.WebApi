using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationForms.Queries.Search;

public class EducationFormsBySearchRequestSpec : EntitiesByPaginationFilterSpec<EducationForm, EducationFormDto>
{
    public EducationFormsBySearchRequestSpec(SearchEducationFormsRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}