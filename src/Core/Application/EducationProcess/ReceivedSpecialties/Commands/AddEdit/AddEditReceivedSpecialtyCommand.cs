using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Commands.AddEdit;

public partial class AddEditReceivedSpecialtyCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public int FsesCategoryPartitionId { get; set; }
    [Required]
    public string Qualification { get; set; } = default!;
}

internal class AddEditReceivedSpecialtyCommandHandler : IRequestHandler<AddEditReceivedSpecialtyCommand, int>
{
    private readonly IStringLocalizer<AddEditReceivedSpecialtyCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditReceivedSpecialtyCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditReceivedSpecialtyCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditReceivedSpecialtyCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var receivedSpecialty = command.Adapt<ReceivedSpecialty>();
            await _unitOfWork.Repository<ReceivedSpecialty>().AddAsync(receivedSpecialty);
            await _unitOfWork.Commit(cancellationToken);
            return receivedSpecialty.Id;
        }
        else
        {
            var receivedSpecialty = await _unitOfWork.Repository<ReceivedSpecialty>().GetByIdAsync(command.Id);

            if (receivedSpecialty is null)
            {
                throw new NotFoundException("Received Specialty Not Found!");
            }

            receivedSpecialty.FsesCategoryPartitionId = command.FsesCategoryPartitionId;
            receivedSpecialty.Qualification = command.Qualification;

            await _unitOfWork.Repository<ReceivedSpecialty>().UpdateAsync(receivedSpecialty);
            await _unitOfWork.Commit(cancellationToken);
            return receivedSpecialty.Id;
        }
    }
}
