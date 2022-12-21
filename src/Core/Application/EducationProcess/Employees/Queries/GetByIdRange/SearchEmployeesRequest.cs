using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Employees.Queries.GetByIdRange;

public class SearchEmployeesByIdRangeRequest : BaseFilter, IRequest<ICollection<EmployeeDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchEmployeesByIdRangeRequestHandler : IRequestHandler<SearchEmployeesByIdRangeRequest, ICollection<EmployeeDto>>
{
    private readonly IReadAudRepository<Employee> _repository;

    public SearchEmployeesByIdRangeRequestHandler(IReadAudRepository<Employee> repository) => _repository = repository;

    public async Task<ICollection<EmployeeDto>> Handle(SearchEmployeesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new EmployeesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}