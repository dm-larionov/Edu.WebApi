using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Queries.Search;

public class IntermediateCertificationFormsBySearchRequestSpec : EntitiesByPaginationFilterSpec<IntermediateCertificationForm, IntermediateCertificationFormDto>
{
    public IntermediateCertificationFormsBySearchRequestSpec(SearchIntermediateCertificationFormsRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}