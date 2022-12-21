using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.GetByIdRange;

public class FixedDisciplinesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<FixedDiscipline, FixedDisciplineDto>
{
    public FixedDisciplinesByIdRangeRequestSpec(SearchFixedDisciplinesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}