using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.GetByIdRange;

public class SearchFixedDisciplineStatusesByIdRangeRequest : BaseFilter, IRequest<ICollection<FixedDisciplineStatusDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchFixedDisciplineStatusesByIdRangeRequestHandler : IRequestHandler<SearchFixedDisciplineStatusesByIdRangeRequest, ICollection<FixedDisciplineStatusDto>>
{
    private readonly IReadAudRepository<FixedDisciplineStatus> _repository;

    public SearchFixedDisciplineStatusesByIdRangeRequestHandler(IReadAudRepository<FixedDisciplineStatus> repository) => _repository = repository;

    public async Task<ICollection<FixedDisciplineStatusDto>> Handle(SearchFixedDisciplineStatusesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new FixedDisciplineStatusesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}