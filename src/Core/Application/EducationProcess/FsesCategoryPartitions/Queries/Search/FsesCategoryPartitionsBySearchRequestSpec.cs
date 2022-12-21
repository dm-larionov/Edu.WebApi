using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.Search;

public class FsesCategoryPartitionsBySearchRequestSpec : EntitiesByPaginationFilterSpec<FsesCategoryPartition, FsesCategoryPartitionDto>
{
    public FsesCategoryPartitionsBySearchRequestSpec(SearchFsesCategoryPartitionsRequest request)
        : base(request) =>
        Query
            .Where(x => true);
}