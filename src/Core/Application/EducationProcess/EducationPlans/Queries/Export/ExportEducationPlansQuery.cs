using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Application.Interfaces.Services;
using Edu.WebApi.Domain.EducationProcess;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.Export;

public class ExportEducationPlansQuery : IRequest<Stream>
{
    public int EducationPlanId { get; set; }
}

internal class ExportEducationPlansQueryHandler : IRequestHandler<ExportEducationPlansQuery, Stream>
{
    private readonly IExcelEducationPlanService _excelEducationPlanService;
    private readonly IUnitOfWork<int> _unitOfWork;
    private readonly IStringLocalizer<ExportEducationPlansQueryHandler> _localizer;

    public ExportEducationPlansQueryHandler(IExcelEducationPlanService excelEducationPlanService, IUnitOfWork<int> unitOfWork, IStringLocalizer<ExportEducationPlansQueryHandler> localizer)
    {
        _excelEducationPlanService = excelEducationPlanService;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<Stream> Handle(ExportEducationPlansQuery request, CancellationToken cancellationToken)
    {
        var disciplineSemesters = await _unitOfWork.Repository<DisciplineSemester>().Entities
            .Where(x => x.EducationPlans.Any(x => x.Id == request.EducationPlanId))
            .Include(x => x.Discipline)
            .ThenInclude(x => x.EducationCycle)
            .Include(x => x.Discipline)
            .ThenInclude(x => x.EducationModule)
            .Include(x => x.CertificationForm)
            .ToListAsync(cancellationToken: cancellationToken);
        return await _excelEducationPlanService.ExportAsync(disciplineSemesters);
    }
}
