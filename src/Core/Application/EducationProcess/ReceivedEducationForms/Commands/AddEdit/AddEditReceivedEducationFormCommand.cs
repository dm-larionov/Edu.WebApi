using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Commands.AddEdit;

public partial class AddEditReceivedEducationFormCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public int EducationFormId { get; set; }
    public string? AdditionalInfo { get; set; }
}

internal class AddEditReceivedEducationFormCommandHandler : IRequestHandler<AddEditReceivedEducationFormCommand, int>
{
    private readonly IStringLocalizer<AddEditReceivedEducationFormCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditReceivedEducationFormCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditReceivedEducationFormCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditReceivedEducationFormCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var receivedEducationForm = command.Adapt<ReceivedEducationForm>();
            await _unitOfWork.Repository<ReceivedEducationForm>().AddAsync(receivedEducationForm);
            await _unitOfWork.Commit(cancellationToken);
            return receivedEducationForm.Id;
        }
        else
        {
            var receivedEducationForm = await _unitOfWork.Repository<ReceivedEducationForm>().GetByIdAsync(command.Id);

            if (receivedEducationForm is null)
            {
                throw new NotFoundException("Received Education Form Not Found!");
            }

            receivedEducationForm.EducationFormId = command.EducationFormId;
            receivedEducationForm.AdditionalInfo = command.AdditionalInfo;

            await _unitOfWork.Repository<ReceivedEducationForm>().UpdateAsync(receivedEducationForm);
            await _unitOfWork.Commit(cancellationToken);
            return receivedEducationForm.Id;
        }
    }
}
