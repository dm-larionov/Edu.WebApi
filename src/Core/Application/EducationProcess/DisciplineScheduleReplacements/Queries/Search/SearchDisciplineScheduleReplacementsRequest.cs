using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.Search;

public class SearchDisciplineScheduleReplacementsRequest : PaginationFilter, IRequest<PaginationResponse<DisciplineScheduleReplacementDto>>
{
}

public class SearchDisciplineScheduleReplacementsRequestHandler : IRequestHandler<SearchDisciplineScheduleReplacementsRequest, PaginationResponse<DisciplineScheduleReplacementDto>>
{
    private readonly IReadAudRepository<DisciplineScheduleReplacement> _repository;

    public SearchDisciplineScheduleReplacementsRequestHandler(IReadAudRepository<DisciplineScheduleReplacement> repository) => _repository = repository;

    public async Task<PaginationResponse<DisciplineScheduleReplacementDto>> Handle(SearchDisciplineScheduleReplacementsRequest request, CancellationToken cancellationToken)
    {
        var spec = new DisciplineScheduleReplacementsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}