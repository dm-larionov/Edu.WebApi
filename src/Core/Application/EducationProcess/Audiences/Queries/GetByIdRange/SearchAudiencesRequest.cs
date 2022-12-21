using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Audiences.Queries.GetByIdRange;

public class SearchAudiencesByIdRangeRequest : BaseFilter, IRequest<ICollection<AudienceDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchAudiencesByIdRangeRequestHandler : IRequestHandler<SearchAudiencesByIdRangeRequest, ICollection<AudienceDto>>
{
    private readonly IReadAudRepository<Audience> _repository;

    public SearchAudiencesByIdRangeRequestHandler(IReadAudRepository<Audience> repository) => _repository = repository;

    public async Task<ICollection<AudienceDto>> Handle(SearchAudiencesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new AudiencesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}