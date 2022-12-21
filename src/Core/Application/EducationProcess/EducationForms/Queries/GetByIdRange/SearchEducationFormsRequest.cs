using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationForms.Queries.GetByIdRange;

public class SearchEducationFormsByIdRangeRequest : BaseFilter, IRequest<ICollection<EducationFormDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchEducationFormsByIdRangeRequestHandler : IRequestHandler<SearchEducationFormsByIdRangeRequest, ICollection<EducationFormDto>>
{
    private readonly IReadAudRepository<EducationForm> _repository;

    public SearchEducationFormsByIdRangeRequestHandler(IReadAudRepository<EducationForm> repository) => _repository = repository;

    public async Task<ICollection<EducationFormDto>> Handle(SearchEducationFormsByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new EducationFormsByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}