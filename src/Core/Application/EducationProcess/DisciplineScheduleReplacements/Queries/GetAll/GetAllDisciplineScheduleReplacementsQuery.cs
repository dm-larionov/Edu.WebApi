using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.GetAll;

public class GetAllDisciplineScheduleReplacementsQuery : IRequest<List<GetAllDisciplineScheduleReplacementsResponse>>
{
    public GetAllDisciplineScheduleReplacementsQuery()
    {
    }
}

internal class GetAllDisciplineScheduleReplacementsCachedQueryHandler : IRequestHandler<GetAllDisciplineScheduleReplacementsQuery, List<GetAllDisciplineScheduleReplacementsResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllDisciplineScheduleReplacementsCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllDisciplineScheduleReplacementsResponse>> Handle(GetAllDisciplineScheduleReplacementsQuery request, CancellationToken cancellationToken)
    {
        var disciplineScheduleReplacements = await _unitOfWork.Repository<DisciplineScheduleReplacement>().GetAllAsync();
        return disciplineScheduleReplacements.Adapt<List<GetAllDisciplineScheduleReplacementsResponse>>();
    }
}
