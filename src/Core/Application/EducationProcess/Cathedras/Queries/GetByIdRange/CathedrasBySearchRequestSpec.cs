using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Cathedras.Queries.GetByIdRange;

public class CathedrasByIdRangeRequestSpec : EntitiesByBaseFilterSpec<Cathedra, CathedraDto>
{
    public CathedrasByIdRangeRequestSpec(SearchCathedrasByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}