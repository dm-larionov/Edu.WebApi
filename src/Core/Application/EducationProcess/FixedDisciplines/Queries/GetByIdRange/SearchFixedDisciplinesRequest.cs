using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.GetByIdRange;

public class SearchFixedDisciplinesByIdRangeRequest : BaseFilter, IRequest<ICollection<FixedDisciplineDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchFixedDisciplinesByIdRangeRequestHandler : IRequestHandler<SearchFixedDisciplinesByIdRangeRequest, ICollection<FixedDisciplineDto>>
{
    private readonly IReadAudRepository<FixedDiscipline> _repository;

    public SearchFixedDisciplinesByIdRangeRequestHandler(IReadAudRepository<FixedDiscipline> repository) => _repository = repository;

    public async Task<ICollection<FixedDisciplineDto>> Handle(SearchFixedDisciplinesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new FixedDisciplinesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}