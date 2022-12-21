using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Employees.Commands.Delete;

public class DeleteEmployeeCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, int>
{
    private readonly IStringLocalizer<DeleteEmployeeCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteEmployeeCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteEmployeeCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
    {
        var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(command.Id);

        if (employee is null)
        {
            throw new NotFoundException("Employee Not Found!");
        }

        await _unitOfWork.Repository<Employee>().DeleteAsync(employee);
        await _unitOfWork.Commit(cancellationToken);
        return employee.Id;
    }
}
