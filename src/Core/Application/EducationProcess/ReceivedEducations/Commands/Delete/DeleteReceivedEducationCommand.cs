using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducations.Commands.Delete;

public class DeleteReceivedEducationCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteReceivedEducationCommandHandler : IRequestHandler<DeleteReceivedEducationCommand, int>
{
    private readonly IStringLocalizer<DeleteReceivedEducationCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteReceivedEducationCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteReceivedEducationCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteReceivedEducationCommand command, CancellationToken cancellationToken)
    {
        var receivedEducation = await _unitOfWork.Repository<ReceivedEducation>().GetByIdAsync(command.Id);

        if (receivedEducation is null)
        {
            throw new NotFoundException("Received Education Not Found!");
        }

        await _unitOfWork.Repository<ReceivedEducation>().DeleteAsync(receivedEducation);
        await _unitOfWork.Commit(cancellationToken);
        return receivedEducation.Id;
    }
}
