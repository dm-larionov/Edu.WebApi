using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.StudentGroups.Commands.Delete;

public class DeleteStudentGroupCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteStudentGroupCommandHandler : IRequestHandler<DeleteStudentGroupCommand, int>
{
    private readonly IStringLocalizer<DeleteStudentGroupCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteStudentGroupCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteStudentGroupCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteStudentGroupCommand command, CancellationToken cancellationToken)
    {
        var studentGroup = await _unitOfWork.Repository<StudentGroup>().GetByIdAsync(command.Id);

        if (studentGroup is null)
        {
            throw new NotFoundException("Student Group Not Found!");
        }

        await _unitOfWork.Repository<StudentGroup>().DeleteAsync(studentGroup);
        await _unitOfWork.Commit(cancellationToken);
        return studentGroup.Id;
    }
}
