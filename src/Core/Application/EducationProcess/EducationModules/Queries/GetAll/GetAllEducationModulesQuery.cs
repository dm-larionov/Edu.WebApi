using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationModules.Queries.GetAll;

public class GetAllEducationModulesQuery : IRequest<List<GetAllEducationModulesResponse>>
{
    public GetAllEducationModulesQuery()
    {
    }
}

internal class GetAllEducationModulesCachedQueryHandler : IRequestHandler<GetAllEducationModulesQuery, List<GetAllEducationModulesResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllEducationModulesCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllEducationModulesResponse>> Handle(GetAllEducationModulesQuery request, CancellationToken cancellationToken)
    {
        var educationModules = await _unitOfWork.Repository<EducationModule>().GetAllAsync();
        return educationModules.Adapt<List<GetAllEducationModulesResponse>>();
    }
}
