using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Commands.Delete;

public class DeleteReceivedSpecialtyCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteReceivedSpecialtyCommandHandler : IRequestHandler<DeleteReceivedSpecialtyCommand, int>
{
    private readonly IStringLocalizer<DeleteReceivedSpecialtyCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteReceivedSpecialtyCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteReceivedSpecialtyCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteReceivedSpecialtyCommand command, CancellationToken cancellationToken)
    {
        var receivedSpecialty = await _unitOfWork.Repository<ReceivedSpecialty>().GetByIdAsync(command.Id);

        if (receivedSpecialty is null)
        {
            throw new NotFoundException("Received Specialty Not Found!");
        }

        await _unitOfWork.Repository<ReceivedSpecialty>().DeleteAsync(receivedSpecialty);
        await _unitOfWork.Commit(cancellationToken);
        return receivedSpecialty.Id;
    }
}
