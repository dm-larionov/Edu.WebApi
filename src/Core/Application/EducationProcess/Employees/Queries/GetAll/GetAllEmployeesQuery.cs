using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Employees.Queries.GetAll;

public class GetAllEmployeesQuery : IRequest<List<GetAllEmployeesResponse>>
{
    public GetAllEmployeesQuery()
    {
    }
}

internal class GetAllEmployeesCachedQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<GetAllEmployeesResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllEmployeesCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllEmployeesResponse>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = await _unitOfWork.Repository<Employee>().GetAllAsync();
        return employees.Adapt<List<GetAllEmployeesResponse>>();
    }
}
