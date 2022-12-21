using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Employees.Queries.Search;

public class SearchEmployeesRequest : PaginationFilter, IRequest<PaginationResponse<EmployeeDto>>
{
}

public class SearchEmployeesRequestHandler : IRequestHandler<SearchEmployeesRequest, PaginationResponse<EmployeeDto>>
{
    private readonly IReadAudRepository<Employee> _repository;

    public SearchEmployeesRequestHandler(IReadAudRepository<Employee> repository) => _repository = repository;

    public async Task<PaginationResponse<EmployeeDto>> Handle(SearchEmployeesRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}