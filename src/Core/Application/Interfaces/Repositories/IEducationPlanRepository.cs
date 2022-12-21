using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.Interfaces.Repositories;

public interface IEducationPlanRepository
{
    Task AddDisciplineSemester(int educationPlanId, DisciplineSemester disciplineSemester);
    Task AddRangeDisciplineSemester(int eduactionPlanId, IReadOnlyCollection<DisciplineSemester> disciplineSemesters);
}
