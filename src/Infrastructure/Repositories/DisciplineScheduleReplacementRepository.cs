using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Infrastructure.Repositories;

public class DisciplineScheduleReplacementRepository : IDisciplineScheduleReplacementRepository
{
    private readonly IRepositoryAsync<DisciplineScheduleReplacement, int> _repository;

    public DisciplineScheduleReplacementRepository(IRepositoryAsync<DisciplineScheduleReplacement, int> repository)
    {
        _repository = repository;
    }

    public async Task<DisciplineScheduleReplacement[]> GetAllForDayAsync(DateTime date)
    {
        return await _repository.Entities.Where(x => x.Date.ToShortDateString() == date.ToShortDateString())
            .ToArrayAsync();
    }
}
