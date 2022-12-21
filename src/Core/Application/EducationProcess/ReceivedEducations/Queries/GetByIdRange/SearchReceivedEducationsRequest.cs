using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.GetByIdRange;

public class SearchReceivedEducationsByIdRangeRequest : BaseFilter, IRequest<ICollection<ReceivedEducationDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchReceivedEducationsByIdRangeRequestHandler : IRequestHandler<SearchReceivedEducationsByIdRangeRequest, ICollection<ReceivedEducationDto>>
{
    private readonly IReadAudRepository<ReceivedEducation> _repository;

    public SearchReceivedEducationsByIdRangeRequestHandler(IReadAudRepository<ReceivedEducation> repository) => _repository = repository;

    public async Task<ICollection<ReceivedEducationDto>> Handle(SearchReceivedEducationsByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new ReceivedEducationsByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}