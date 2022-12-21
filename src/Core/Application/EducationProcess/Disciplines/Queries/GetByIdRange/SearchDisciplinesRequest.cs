using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Disciplines.Queries.GetByIdRange;

public class SearchDisciplinesByIdRangeRequest : BaseFilter, IRequest<ICollection<DisciplineDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchDisciplinesByIdRangeRequestHandler : IRequestHandler<SearchDisciplinesByIdRangeRequest, ICollection<DisciplineDto>>
{
    private readonly IReadAudRepository<Discipline> _repository;

    public SearchDisciplinesByIdRangeRequestHandler(IReadAudRepository<Discipline> repository) => _repository = repository;

    public async Task<ICollection<DisciplineDto>> Handle(SearchDisciplinesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new DisciplinesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}