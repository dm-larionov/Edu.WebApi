using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.GetByIdRange;

public class SearchFsesCategoryPartitionsByIdRangeRequest : BaseFilter, IRequest<ICollection<FsesCategoryPartitionDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchFsesCategoryPartitionsByIdRangeRequestHandler : IRequestHandler<SearchFsesCategoryPartitionsByIdRangeRequest, ICollection<FsesCategoryPartitionDto>>
{
    private readonly IReadAudRepository<FsesCategoryPartition> _repository;

    public SearchFsesCategoryPartitionsByIdRangeRequestHandler(IReadAudRepository<FsesCategoryPartition> repository) => _repository = repository;

    public async Task<ICollection<FsesCategoryPartitionDto>> Handle(SearchFsesCategoryPartitionsByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new FsesCategoryPartitionsByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}