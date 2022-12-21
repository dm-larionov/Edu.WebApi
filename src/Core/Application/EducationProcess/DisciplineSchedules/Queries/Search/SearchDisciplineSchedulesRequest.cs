using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.Search;

public class SearchDisciplineSchedulesRequest : PaginationFilter, IRequest<PaginationResponse<DisciplineScheduleDto>>
{
    public int? FixedDisciplineId { get; set; }
}

public class SearchDisciplineSchedulesRequestHandler : IRequestHandler<SearchDisciplineSchedulesRequest, PaginationResponse<DisciplineScheduleDto>>
{
    private readonly IReadAudRepository<DisciplineSchedule> _repository;

    public SearchDisciplineSchedulesRequestHandler(IReadAudRepository<DisciplineSchedule> repository) => _repository = repository;

    public async Task<PaginationResponse<DisciplineScheduleDto>> Handle(SearchDisciplineSchedulesRequest request, CancellationToken cancellationToken)
    {
        var spec = new DisciplineSchedulesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}