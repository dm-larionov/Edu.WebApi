using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Commands.Delete;

public class DeleteStudentGroupNameChangeCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteStudentGroupNameChangeCommandHandler : IRequestHandler<DeleteStudentGroupNameChangeCommand, int>
{
    private readonly IStringLocalizer<DeleteStudentGroupNameChangeCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteStudentGroupNameChangeCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteStudentGroupNameChangeCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteStudentGroupNameChangeCommand command, CancellationToken cancellationToken)
    {
        var studentGroupNameChange = await _unitOfWork.Repository<StudentGroupNameChange>().GetByIdAsync(command.Id);

        if (studentGroupNameChange is null)
        {
            throw new NotFoundException("Student Group Name Change Not Found!");
        }

        await _unitOfWork.Repository<StudentGroupNameChange>().DeleteAsync(studentGroupNameChange);
        await _unitOfWork.Commit(cancellationToken);
        return studentGroupNameChange.Id;
    }
}
