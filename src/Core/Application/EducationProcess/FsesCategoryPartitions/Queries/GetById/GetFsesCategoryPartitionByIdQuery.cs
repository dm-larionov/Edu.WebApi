using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.GetById;

public class GetFsesCategoryPartitionByIdQuery : IRequest<GetFsesCategoryPartitionByIdResponse>
{
    public int Id { get; set; }
}

internal class GetFsesCategoryPartitionByIdQueryHandler : IRequestHandler<GetFsesCategoryPartitionByIdQuery, GetFsesCategoryPartitionByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetFsesCategoryPartitionByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetFsesCategoryPartitionByIdResponse> Handle(GetFsesCategoryPartitionByIdQuery query, CancellationToken cancellationToken)
    {
        var fsesCategoryPartition = await _unitOfWork.Repository<FsesCategoryPartition>().GetByIdAsync(query.Id);
        return fsesCategoryPartition.Adapt<GetFsesCategoryPartitionByIdResponse>();
    }
}
