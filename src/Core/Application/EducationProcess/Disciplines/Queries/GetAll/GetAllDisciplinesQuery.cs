using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Disciplines.Queries.GetAll;

public class GetAllDisciplinesQuery : IRequest<List<GetAllDisciplinesResponse>>
{
    public GetAllDisciplinesQuery()
    {
    }
}

internal class GetAllDisciplinesCachedQueryHandler : IRequestHandler<GetAllDisciplinesQuery, List<GetAllDisciplinesResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllDisciplinesCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllDisciplinesResponse>> Handle(GetAllDisciplinesQuery request, CancellationToken cancellationToken)
    {
        var disciplines = await _unitOfWork.Repository<Discipline>().GetAllAsync();
        return disciplines.Adapt<List<GetAllDisciplinesResponse>>();
    }
}
