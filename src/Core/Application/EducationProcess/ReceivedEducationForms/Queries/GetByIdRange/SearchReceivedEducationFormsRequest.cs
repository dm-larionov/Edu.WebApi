using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.GetByIdRange;

public class SearchReceivedEducationFormsByIdRangeRequest : BaseFilter, IRequest<ICollection<ReceivedEducationFormDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchReceivedEducationFormsByIdRangeRequestHandler : IRequestHandler<SearchReceivedEducationFormsByIdRangeRequest, ICollection<ReceivedEducationFormDto>>
{
    private readonly IReadAudRepository<ReceivedEducationForm> _repository;

    public SearchReceivedEducationFormsByIdRangeRequestHandler(IReadAudRepository<ReceivedEducationForm> repository) => _repository = repository;

    public async Task<ICollection<ReceivedEducationFormDto>> Handle(SearchReceivedEducationFormsByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new ReceivedEducationFormsByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}