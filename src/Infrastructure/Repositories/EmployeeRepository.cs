using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IRepositoryAsync<Employee, int> _repository;

    public EmployeeRepository(IRepositoryAsync<Employee, int> repository)
    {
        _repository = repository;
    }
}
