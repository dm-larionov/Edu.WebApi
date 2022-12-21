using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Commands.AddEdit;

public partial class AddEditStudentGroupNameChangeCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public int StudentGroupId { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public DateTime Date { get; set; }
}

internal class AddEditStudentGroupNameChangeCommandHandler : IRequestHandler<AddEditStudentGroupNameChangeCommand, int>
{
    private readonly IStringLocalizer<AddEditStudentGroupNameChangeCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditStudentGroupNameChangeCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditStudentGroupNameChangeCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditStudentGroupNameChangeCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var studentGroupNameChange = command.Adapt<StudentGroupNameChange>();
            await _unitOfWork.Repository<StudentGroupNameChange>().AddAsync(studentGroupNameChange);
            await _unitOfWork.Commit(cancellationToken);
            return studentGroupNameChange.Id;
        }
        else
        {
            var studentGroupNameChange = await _unitOfWork.Repository<StudentGroupNameChange>().GetByIdAsync(command.Id);

            if (studentGroupNameChange is null)
            {
                throw new NotFoundException("Student Group Name Change Not Found!");
            }

            studentGroupNameChange.StudentGroupId = command.StudentGroupId;
            studentGroupNameChange.Name = command.Name;
            studentGroupNameChange.Date = command.Date;
            await _unitOfWork.Repository<StudentGroupNameChange>().UpdateAsync(studentGroupNameChange);
            await _unitOfWork.Commit(cancellationToken);
            return studentGroupNameChange.Id;
        }
    }
}
