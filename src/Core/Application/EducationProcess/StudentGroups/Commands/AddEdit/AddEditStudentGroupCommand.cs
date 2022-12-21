using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.StudentGroups.Commands.AddEdit;

public partial class AddEditStudentGroupCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public byte CourseNumber { get; set; }
    public int? CuratorId { get; set; }
    [Required]
    public int ReceivedEducationId { get; set; }
    public int? EducationPlanId { get; set; }
    [Required]
    public short ReceiptYear { get; set; }
    [Required]
    public byte StudentQuantity { get; set; }
}

internal class AddEditStudentGroupCommandHandler : IRequestHandler<AddEditStudentGroupCommand, int>
{
    private readonly IStringLocalizer<AddEditStudentGroupCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditStudentGroupCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditStudentGroupCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditStudentGroupCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var studentGroup = command.Adapt<StudentGroup>();
            await _unitOfWork.Repository<StudentGroup>().AddAsync(studentGroup);
            await _unitOfWork.Commit(cancellationToken);
            return studentGroup.Id;
        }
        else
        {
            var studentGroup = await _unitOfWork.Repository<StudentGroup>().GetByIdAsync(command.Id);

            if (studentGroup is null)
            {
                throw new NotFoundException("Student Group Not Found!");
            }

            studentGroup = command.Adapt<StudentGroup>();

            await _unitOfWork.Repository<StudentGroup>().UpdateAsync(studentGroup);
            await _unitOfWork.Commit(cancellationToken);
            return studentGroup.Id;
        }
    }
}
