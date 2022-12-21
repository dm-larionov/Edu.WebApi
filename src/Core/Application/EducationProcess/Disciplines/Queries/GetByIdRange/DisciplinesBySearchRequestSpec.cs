using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Disciplines.Queries.GetByIdRange;

public class DisciplinesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<Discipline, DisciplineDto>
{
    public DisciplinesByIdRangeRequestSpec(SearchDisciplinesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}