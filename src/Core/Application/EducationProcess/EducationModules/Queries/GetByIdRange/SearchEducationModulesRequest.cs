using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationModules.Queries.GetByIdRange;

public class SearchEducationModulesByIdRangeRequest : BaseFilter, IRequest<ICollection<EducationModuleDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchEducationModulesByIdRangeRequestHandler : IRequestHandler<SearchEducationModulesByIdRangeRequest, ICollection<EducationModuleDto>>
{
    private readonly IReadAudRepository<EducationModule> _repository;

    public SearchEducationModulesByIdRangeRequestHandler(IReadAudRepository<EducationModule> repository) => _repository = repository;

    public async Task<ICollection<EducationModuleDto>> Handle(SearchEducationModulesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new EducationModulesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}