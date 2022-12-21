using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.GetByIdRange;

public class SearchReceivedSpecialtiesByIdRangeRequest : BaseFilter, IRequest<ICollection<ReceivedSpecialtyDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchReceivedSpecialtiesByIdRangeRequestHandler : IRequestHandler<SearchReceivedSpecialtiesByIdRangeRequest, ICollection<ReceivedSpecialtyDto>>
{
    private readonly IReadAudRepository<ReceivedSpecialty> _repository;

    public SearchReceivedSpecialtiesByIdRangeRequestHandler(IReadAudRepository<ReceivedSpecialty> repository) => _repository = repository;

    public async Task<ICollection<ReceivedSpecialtyDto>> Handle(SearchReceivedSpecialtiesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new ReceivedSpecialtiesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}