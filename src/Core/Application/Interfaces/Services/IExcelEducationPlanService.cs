using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.Interfaces.Services;

public interface IExcelEducationPlanService
{
    Task<Stream> ExportAsync(IReadOnlyCollection<DisciplineSemester> disciplineSemesters);
}
