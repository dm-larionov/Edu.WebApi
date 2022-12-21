using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.GetAll;

public class GetAllFsesCategoryPartitionsQuery : IRequest<List<GetAllFsesCategoryPartitionsResponse>>
{
    public GetAllFsesCategoryPartitionsQuery()
    {
    }
}

internal class GetAllFsesCategoryPartitionsCachedQueryHandler : IRequestHandler<GetAllFsesCategoryPartitionsQuery, List<GetAllFsesCategoryPartitionsResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllFsesCategoryPartitionsCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllFsesCategoryPartitionsResponse>> Handle(GetAllFsesCategoryPartitionsQuery request, CancellationToken cancellationToken)
    {
        var fsesCategoryPartitions = await _unitOfWork.Repository<FsesCategoryPartition>().GetAllAsync();
        return fsesCategoryPartitions.Adapt<List<GetAllFsesCategoryPartitionsResponse>>();
    }
}
