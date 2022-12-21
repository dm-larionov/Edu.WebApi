using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationModules.Queries.Search;

public class SearchEducationModulesRequest : PaginationFilter, IRequest<PaginationResponse<EducationModuleDto>>
{
}

public class SearchEducationModulesRequestHandler : IRequestHandler<SearchEducationModulesRequest, PaginationResponse<EducationModuleDto>>
{
    private readonly IReadAudRepository<EducationModule> _repository;

    public SearchEducationModulesRequestHandler(IReadAudRepository<EducationModule> repository) => _repository = repository;

    public async Task<PaginationResponse<EducationModuleDto>> Handle(SearchEducationModulesRequest request, CancellationToken cancellationToken)
    {
        var spec = new EducationModulesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}