using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Cathedras.Queries.GetByIdRange;

public class SearchCathedrasByIdRangeRequest : BaseFilter, IRequest<ICollection<CathedraDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchCathedrasByIdRangeRequestHandler : IRequestHandler<SearchCathedrasByIdRangeRequest, ICollection<CathedraDto>>
{
    private readonly IReadAudRepository<Cathedra> _repository;

    public SearchCathedrasByIdRangeRequestHandler(IReadAudRepository<Cathedra> repository) => _repository = repository;

    public async Task<ICollection<CathedraDto>> Handle(SearchCathedrasByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new CathedrasByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}