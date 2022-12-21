using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.GetAll;

public class GetAllEducationCyclesQuery : IRequest<List<GetAllEducationCyclesResponse>>
{
    public GetAllEducationCyclesQuery()
    {
    }
}

internal class GetAllEducationCyclesCachedQueryHandler : IRequestHandler<GetAllEducationCyclesQuery, List<GetAllEducationCyclesResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllEducationCyclesCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllEducationCyclesResponse>> Handle(GetAllEducationCyclesQuery request, CancellationToken cancellationToken)
    {
        var educationCyclesAndModules = await _unitOfWork.Repository<EducationCycle>().GetAllAsync();
        return educationCyclesAndModules.Adapt<List<GetAllEducationCyclesResponse>>();
    }
}
