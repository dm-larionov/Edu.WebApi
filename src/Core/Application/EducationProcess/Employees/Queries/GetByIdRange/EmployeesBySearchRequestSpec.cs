using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Employees.Queries.GetByIdRange;

public class EmployeesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<Employee, EmployeeDto>
{
    public EmployeesByIdRangeRequestSpec(SearchEmployeesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}