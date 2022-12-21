using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationLevels.Queries.GetAll;

public class GetAllEducationLevelsQuery : IRequest<List<GetAllEducationLevelsResponse>>
{
    public GetAllEducationLevelsQuery()
    {
    }
}

internal class GetAllEducationLevelsCachedQueryHandler : IRequestHandler<GetAllEducationLevelsQuery, List<GetAllEducationLevelsResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllEducationLevelsCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllEducationLevelsResponse>> Handle(GetAllEducationLevelsQuery request, CancellationToken cancellationToken)
    {
        var educationLevels = await _unitOfWork.Repository<EducationLevel>().GetAllAsync();
        return educationLevels.Adapt<List<GetAllEducationLevelsResponse>>();
    }
}
