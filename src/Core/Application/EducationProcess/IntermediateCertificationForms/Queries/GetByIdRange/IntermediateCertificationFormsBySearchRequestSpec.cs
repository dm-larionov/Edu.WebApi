using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Queries.GetByIdRange;

public class IntermediateCertificationFormsByIdRangeRequestSpec : EntitiesByBaseFilterSpec<IntermediateCertificationForm, IntermediateCertificationFormDto>
{
    public IntermediateCertificationFormsByIdRangeRequestSpec(SearchIntermediateCertificationFormsByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}