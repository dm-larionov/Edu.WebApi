using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.Interfaces.Repositories;

public interface IStudentGroupRepository
{
    Task<StudentGroup[]> GetAllExistingByDate(DateTime date);
}
