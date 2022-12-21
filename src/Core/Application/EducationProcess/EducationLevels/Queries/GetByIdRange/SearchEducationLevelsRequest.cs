using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationLevels.Queries.GetByIdRange;

public class SearchEducationLevelsByIdRangeRequest : BaseFilter, IRequest<ICollection<EducationLevelDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchEducationLevelsByIdRangeRequestHandler : IRequestHandler<SearchEducationLevelsByIdRangeRequest, ICollection<EducationLevelDto>>
{
    private readonly IReadAudRepository<EducationLevel> _repository;

    public SearchEducationLevelsByIdRangeRequestHandler(IReadAudRepository<EducationLevel> repository) => _repository = repository;

    public async Task<ICollection<EducationLevelDto>> Handle(SearchEducationLevelsByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new EducationLevelsByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}