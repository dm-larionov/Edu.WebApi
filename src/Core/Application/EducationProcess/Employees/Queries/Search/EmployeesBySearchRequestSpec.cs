using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Employees.Queries.Search;

public class EmployeesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Employee, EmployeeDto>
{
    public EmployeesBySearchRequestSpec(SearchEmployeesRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}