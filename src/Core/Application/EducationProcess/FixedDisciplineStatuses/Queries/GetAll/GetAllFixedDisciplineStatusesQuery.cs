using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.GetAll;

public class GetAllFixedDisciplineStatusesQuery : IRequest<List<GetAllFixedDisciplineStatusesResponse>>
{
    public GetAllFixedDisciplineStatusesQuery()
    {
    }
}

internal class GetAllFixedDisciplineStatusesCachedQueryHandler : IRequestHandler<GetAllFixedDisciplineStatusesQuery, List<GetAllFixedDisciplineStatusesResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllFixedDisciplineStatusesCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllFixedDisciplineStatusesResponse>> Handle(GetAllFixedDisciplineStatusesQuery request, CancellationToken cancellationToken)
    {
        var fixedDisciplineStatuses = await _unitOfWork.Repository<FixedDiscipline>().GetAllAsync();
        return fixedDisciplineStatuses.Adapt<List<GetAllFixedDisciplineStatusesResponse>>();
    }
}
