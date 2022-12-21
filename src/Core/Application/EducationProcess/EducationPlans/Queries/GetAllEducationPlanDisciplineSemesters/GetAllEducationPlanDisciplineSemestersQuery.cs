using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.GetAllEducationPlanDisciplineSemesters;

public class GetAllEducationPlanDisciplineSemestersQuery : IRequest<List<GetAllEducationPlanDisciplineSemestersResponse>>
{
    public int EducationPlanId { get; set; }
}

internal class GetAllEducationPlansCachedQueryHandler : IRequestHandler<GetAllEducationPlanDisciplineSemestersQuery, List<GetAllEducationPlanDisciplineSemestersResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllEducationPlansCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllEducationPlanDisciplineSemestersResponse>> Handle(GetAllEducationPlanDisciplineSemestersQuery request, CancellationToken cancellationToken)
    {
        var disciplineSemesters = await _unitOfWork.Repository<DisciplineSemester>().Entities
            .Where(x => x.EducationPlans.Any(x => x.Id == request.EducationPlanId))
            .ToListAsync(cancellationToken: cancellationToken);
        return disciplineSemesters.Adapt<List<GetAllEducationPlanDisciplineSemestersResponse>>();
    }
}
