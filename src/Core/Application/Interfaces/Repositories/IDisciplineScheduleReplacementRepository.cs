using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.Interfaces.Repositories;

public interface IDisciplineScheduleReplacementRepository
{
    Task<DisciplineScheduleReplacement[]> GetAllForDayAsync(DateTime date);
}
