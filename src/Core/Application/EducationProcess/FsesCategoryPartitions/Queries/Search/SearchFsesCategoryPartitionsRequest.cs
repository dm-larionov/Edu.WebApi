using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.Search;

public class SearchFsesCategoryPartitionsRequest : PaginationFilter, IRequest<PaginationResponse<FsesCategoryPartitionDto>>
{
}

public class SearchFsesCategoryPartitionsRequestHandler : IRequestHandler<SearchFsesCategoryPartitionsRequest, PaginationResponse<FsesCategoryPartitionDto>>
{
    private readonly IReadAudRepository<FsesCategoryPartition> _repository;

    public SearchFsesCategoryPartitionsRequestHandler(IReadAudRepository<FsesCategoryPartition> repository) => _repository = repository;

    public async Task<PaginationResponse<FsesCategoryPartitionDto>> Handle(SearchFsesCategoryPartitionsRequest request, CancellationToken cancellationToken)
    {
        var spec = new FsesCategoryPartitionsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}