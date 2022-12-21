using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.GetByIdRange;

public class SearchEducationCyclesByIdRangeRequest : BaseFilter, IRequest<ICollection<EducationCycleDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchEducationCyclesByIdRangeRequestHandler : IRequestHandler<SearchEducationCyclesByIdRangeRequest, ICollection<EducationCycleDto>>
{
    private readonly IReadAudRepository<EducationCycle> _repository;

    public SearchEducationCyclesByIdRangeRequestHandler(IReadAudRepository<EducationCycle> repository) => _repository = repository;

    public async Task<ICollection<EducationCycleDto>> Handle(SearchEducationCyclesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new EducationCyclesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}