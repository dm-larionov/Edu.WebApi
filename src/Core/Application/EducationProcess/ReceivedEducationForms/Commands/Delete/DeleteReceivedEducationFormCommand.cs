using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Commands.Delete;

public class DeleteReceivedEducationFormCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteReceivedEducationFormCommandHandler : IRequestHandler<DeleteReceivedEducationFormCommand, int>
{
    private readonly IStringLocalizer<DeleteReceivedEducationFormCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteReceivedEducationFormCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteReceivedEducationFormCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteReceivedEducationFormCommand command, CancellationToken cancellationToken)
    {
        var receivedEducationForm = await _unitOfWork.Repository<ReceivedEducationForm>().GetByIdAsync(command.Id);

        if (receivedEducationForm is null)
        {
            throw new NotFoundException("ReceivedEducationForm Not Found!");
        }

        await _unitOfWork.Repository<ReceivedEducationForm>().DeleteAsync(receivedEducationForm);
        await _unitOfWork.Commit(cancellationToken);
        return receivedEducationForm.Id;
    }
}
