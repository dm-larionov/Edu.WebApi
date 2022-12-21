using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.GetByIdRange;

public class SearchDisciplineScheduleReplacementsByIdRangeRequest : BaseFilter, IRequest<ICollection<DisciplineScheduleReplacementDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchDisciplineScheduleReplacementsByIdRangeRequestHandler : IRequestHandler<SearchDisciplineScheduleReplacementsByIdRangeRequest, ICollection<DisciplineScheduleReplacementDto>>
{
    private readonly IReadAudRepository<DisciplineScheduleReplacement> _repository;

    public SearchDisciplineScheduleReplacementsByIdRangeRequestHandler(IReadAudRepository<DisciplineScheduleReplacement> repository) => _repository = repository;

    public async Task<ICollection<DisciplineScheduleReplacementDto>> Handle(SearchDisciplineScheduleReplacementsByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new DisciplineScheduleReplacementsByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}