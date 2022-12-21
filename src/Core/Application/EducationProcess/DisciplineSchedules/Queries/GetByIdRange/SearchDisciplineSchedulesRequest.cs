using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetByIdRange;

public class SearchDisciplineSchedulesByIdRangeRequest : BaseFilter, IRequest<ICollection<DisciplineScheduleDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchDisciplineSchedulesByIdRangeRequestHandler : IRequestHandler<SearchDisciplineSchedulesByIdRangeRequest, ICollection<DisciplineScheduleDto>>
{
    private readonly IReadAudRepository<DisciplineSchedule> _repository;

    public SearchDisciplineSchedulesByIdRangeRequestHandler(IReadAudRepository<DisciplineSchedule> repository) => _repository = repository;

    public async Task<ICollection<DisciplineScheduleDto>> Handle(SearchDisciplineSchedulesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new DisciplineSchedulesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}