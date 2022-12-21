using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.AudienceTypes.Queries.GetByIdRange;

public class SearchAudienceTypesByIdRangeRequest : BaseFilter, IRequest<ICollection<AudienceTypeDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchAudienceTypesByIdRangeRequestHandler : IRequestHandler<SearchAudienceTypesByIdRangeRequest, ICollection<AudienceTypeDto>>
{
    private readonly IReadAudRepository<AudienceType> _repository;

    public SearchAudienceTypesByIdRangeRequestHandler(IReadAudRepository<AudienceType> repository) => _repository = repository;

    public async Task<ICollection<AudienceTypeDto>> Handle(SearchAudienceTypesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new AudienceTypesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}