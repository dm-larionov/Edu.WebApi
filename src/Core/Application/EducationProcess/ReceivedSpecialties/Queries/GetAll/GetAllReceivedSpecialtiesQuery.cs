using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.GetAll;

public class GetAllReceivedSpecialtiesQuery : IRequest<List<GetAllReceivedSpecialtiesResponse>>
{
    public GetAllReceivedSpecialtiesQuery()
    {
    }
}

internal class GetAllReceivedSpecialtiesCachedQueryHandler : IRequestHandler<GetAllReceivedSpecialtiesQuery, List<GetAllReceivedSpecialtiesResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllReceivedSpecialtiesCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllReceivedSpecialtiesResponse>> Handle(GetAllReceivedSpecialtiesQuery request, CancellationToken cancellationToken)
    {
        var receivedSpecialties = await _unitOfWork.Repository<ReceivedSpecialty>().GetAllAsync();
        return receivedSpecialties.Adapt<List<GetAllReceivedSpecialtiesResponse>>();
    }
}
