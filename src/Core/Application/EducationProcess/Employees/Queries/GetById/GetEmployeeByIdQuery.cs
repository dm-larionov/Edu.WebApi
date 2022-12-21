using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Employees.Queries.GetById;

public class GetEmployeeByIdQuery : IRequest<GetEmployeeByIdResponse>
{
    public int Id { get; set; }
}

internal class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetEmployeeByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetEmployeeByIdResponse> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(query.Id);
        return employee.Adapt<GetEmployeeByIdResponse>();
    }
}
