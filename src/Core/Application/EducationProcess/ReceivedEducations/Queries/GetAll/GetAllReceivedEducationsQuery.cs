using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.GetAll;

public class GetAllReceivedEducationsQuery : IRequest<List<GetAllReceivedEducationsResponse>>
{
    public GetAllReceivedEducationsQuery()
    {
    }
}

internal class GetAllReceivedEducationsCachedQueryHandler : IRequestHandler<GetAllReceivedEducationsQuery, List<GetAllReceivedEducationsResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllReceivedEducationsCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllReceivedEducationsResponse>> Handle(GetAllReceivedEducationsQuery request, CancellationToken cancellationToken)
    {
        var receivedEducations = await _unitOfWork.Repository<ReceivedEducation>().GetAllAsync();
        return receivedEducations.Adapt<List<GetAllReceivedEducationsResponse>>();
    }
}
