using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.GetAll;

public class GetAllEducationPlansQuery : IRequest<List<GetAllEducationPlansResponse>>
{
    public GetAllEducationPlansQuery()
    {
    }
}

internal class GetAllEducationPlansCachedQueryHandler : IRequestHandler<GetAllEducationPlansQuery, List<GetAllEducationPlansResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllEducationPlansCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllEducationPlansResponse>> Handle(GetAllEducationPlansQuery request, CancellationToken cancellationToken)
    {
        var educationPlans = await _unitOfWork.Repository<EducationPlan>().GetAllAsync();
        return educationPlans.Adapt<List<GetAllEducationPlansResponse>>();
    }
}
