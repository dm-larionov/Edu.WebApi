using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.Search;

public class FixedDisciplinesBySearchRequestSpec : EntitiesByPaginationFilterSpec<FixedDiscipline, FixedDisciplineDto>
{
    public FixedDisciplinesBySearchRequestSpec(SearchFixedDisciplinesRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}