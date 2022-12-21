using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Disciplines.Queries.Search;

public class DisciplinesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Discipline, DisciplineDto>
{
    public DisciplinesBySearchRequestSpec(SearchDisciplinesRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}