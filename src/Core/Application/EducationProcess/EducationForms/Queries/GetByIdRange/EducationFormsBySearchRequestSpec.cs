using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationForms.Queries.GetByIdRange;

public class EducationFormsByIdRangeRequestSpec : EntitiesByBaseFilterSpec<EducationForm, EducationFormDto>
{
    public EducationFormsByIdRangeRequestSpec(SearchEducationFormsByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}