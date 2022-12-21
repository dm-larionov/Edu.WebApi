using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Cathedras.Queries.Search;

public class CathedrasBySearchRequestSpec : EntitiesByPaginationFilterSpec<Cathedra, CathedraDto>
{
    public CathedrasBySearchRequestSpec(SearchCathedrasRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}