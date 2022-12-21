using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class EducationPlanRepository : IEducationPlanRepository
{
    private readonly IRepositoryAsync<EducationPlan, int> _repository;

    public EducationPlanRepository(IRepositoryAsync<EducationPlan, int> repository)
    {
        _repository = repository;
    }

    public async Task AddDisciplineSemester(int educationPlanId, DisciplineSemester disciplineSemester)
    {
        var educationPlan = await _repository.GetByIdAsync(educationPlanId);
        educationPlan.SemesterDisciplines.Add(disciplineSemester);
        await _repository.UpdateAsync(educationPlan);
    }

    public async Task AddRangeDisciplineSemester(int educationPlanId, IReadOnlyCollection<DisciplineSemester> disciplineSemesters)
    {
        var educationPlan = await _repository.GetByIdAsync(educationPlanId);
        foreach (var disciplineSemester in disciplineSemesters)
        {
            educationPlan.SemesterDisciplines.Add(disciplineSemester);
        }
        await _repository.UpdateAsync(educationPlan);
    }
}
