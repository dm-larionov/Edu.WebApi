using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducations.Commands.AddEdit;

public partial class AddEditReceivedEducationCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public int ReceivedSpecialtyId { get; set; }
    [Required]
    public int ReceivedEducationFormId { get; set; }
    [Required]
    public int EducationLevelId { get; set; }
    [Required]
    public short StudyPeriodMonths { get; set; }
    [Required]
    public bool IsBudget { get; set; }
}

internal class AddEditReceivedEducationCommandHandler : IRequestHandler<AddEditReceivedEducationCommand, int>
{
    private readonly IStringLocalizer<AddEditReceivedEducationCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditReceivedEducationCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditReceivedEducationCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditReceivedEducationCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var receivedEducation = command.Adapt<ReceivedEducation>();
            await _unitOfWork.Repository<ReceivedEducation>().AddAsync(receivedEducation);
            await _unitOfWork.Commit(cancellationToken);
            return receivedEducation.Id;
        }
        else
        {
            var receivedEducation = await _unitOfWork.Repository<ReceivedEducation>().GetByIdAsync(command.Id);

            if (receivedEducation is null)
            {
                throw new NotFoundException("Received Education Not Found!");
            }

            receivedEducation.ReceivedSpecialtyId = command.ReceivedSpecialtyId;
            receivedEducation.ReceivedEducationFormId = command.ReceivedEducationFormId;
            receivedEducation.EducationLevelId = command.EducationLevelId;
            receivedEducation.StudyPeriodMonths = command.StudyPeriodMonths;
            receivedEducation.IsBudget = command.IsBudget;

            await _unitOfWork.Repository<ReceivedEducation>().UpdateAsync(receivedEducation);
            await _unitOfWork.Commit(cancellationToken);
            return receivedEducation.Id;
        }
    }
}
