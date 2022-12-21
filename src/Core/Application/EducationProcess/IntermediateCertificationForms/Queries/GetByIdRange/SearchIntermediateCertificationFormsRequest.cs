using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Queries.GetByIdRange;

public class SearchIntermediateCertificationFormsByIdRangeRequest : BaseFilter, IRequest<ICollection<IntermediateCertificationFormDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchIntermediateCertificationFormsByIdRangeRequestHandler : IRequestHandler<SearchIntermediateCertificationFormsByIdRangeRequest, ICollection<IntermediateCertificationFormDto>>
{
    private readonly IReadAudRepository<IntermediateCertificationForm> _repository;

    public SearchIntermediateCertificationFormsByIdRangeRequestHandler(IReadAudRepository<IntermediateCertificationForm> repository) => _repository = repository;

    public async Task<ICollection<IntermediateCertificationFormDto>> Handle(SearchIntermediateCertificationFormsByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new IntermediateCertificationFormsByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}