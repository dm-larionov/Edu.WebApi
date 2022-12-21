using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Infrastructure.Repositories;

public class StudentGroupRepository : IStudentGroupRepository
{
    private readonly IRepositoryAsync<StudentGroup, int> _repository;

    public StudentGroupRepository(IRepositoryAsync<StudentGroup, int> repository)
    {
        _repository = repository;
    }

    public async Task<StudentGroup[]> GetAllExistingByDate(DateTime date)
    {
        StudentGroup[] studentGroups = await _repository.Entities
            .Where(x => x.ReceiptYear <= date.Year &&
                        x.ReceivedEducation.StudyPeriodMonths >= (date.Year - x.ReceiptYear) * 12 + date.Month)
            .AsNoTracking()
            .ToArrayAsync();
        return studentGroups;
    }
}
