using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.GetAll;

public class GetAllFixedDisciplinesQuery : IRequest<List<GetAllFixedDisciplinesResponse>>
{
    public GetAllFixedDisciplinesQuery()
    {
    }
}

internal class GetAllFixedDisciplinesCachedQueryHandler : IRequestHandler<GetAllFixedDisciplinesQuery, List<GetAllFixedDisciplinesResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllFixedDisciplinesCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllFixedDisciplinesResponse>> Handle(GetAllFixedDisciplinesQuery request, CancellationToken cancellationToken)
    {
        var fixedDisciplines = await _unitOfWork.Repository<FixedDiscipline>().GetAllAsync();
        return fixedDisciplines.Adapt<List<GetAllFixedDisciplinesResponse>>();
    }
}
