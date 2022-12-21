using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class StudentGroupNameChangeRepository : IStudentGroupNameChangeRepository
{
    private readonly IRepositoryAsync<StudentGroupNameChange, int> _repository;

    public StudentGroupNameChangeRepository(IRepositoryAsync<StudentGroupNameChange, int> repository)
    {
        _repository = repository;
    }
}
