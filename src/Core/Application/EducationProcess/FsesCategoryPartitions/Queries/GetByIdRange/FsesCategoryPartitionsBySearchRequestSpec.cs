using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.GetByIdRange;

public class FsesCategoryPartitionsByIdRangeRequestSpec : EntitiesByBaseFilterSpec<FsesCategoryPartition, FsesCategoryPartitionDto>
{
    public FsesCategoryPartitionsByIdRangeRequestSpec(SearchFsesCategoryPartitionsByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}