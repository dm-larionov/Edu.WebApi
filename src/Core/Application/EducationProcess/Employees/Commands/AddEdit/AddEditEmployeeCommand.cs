using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Employees.Commands.AddEdit;

public partial class AddEditEmployeeCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string Firstname { get; set; } = default!;
    [Required]
    public string Lastname { get; set; } = default!;
    public string? Middlename { get; set; }
    [Required]
    public int PostId { get; set; }
}

internal class AddEditEmployeeCommandHandler : IRequestHandler<AddEditEmployeeCommand, int>
{
    private readonly IStringLocalizer<AddEditEmployeeCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditEmployeeCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditEmployeeCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditEmployeeCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var employee = command.Adapt<Employee>();
            await _unitOfWork.Repository<Employee>().AddAsync(employee);
            await _unitOfWork.Commit(cancellationToken);
            return employee.Id;
        }
        else
        {
            var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(command.Id);

            if (employee is null)
            {
                throw new NotFoundException("Employee Not Found!");
            }

            employee.Firstname = command.Firstname;
            employee.Lastname = command.Lastname;
            employee.Middlename = command.Middlename;
            employee.PostId = command.PostId;

            await _unitOfWork.Repository<Employee>().UpdateAsync(employee);
            await _unitOfWork.Commit(cancellationToken);
            return employee.Id;
        }
    }
}
