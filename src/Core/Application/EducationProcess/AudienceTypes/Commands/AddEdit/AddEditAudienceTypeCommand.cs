using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.AudienceTypes.Commands.AddEdit;

public partial class AddEditAudienceTypeCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
}

internal class AddEditAudienceTypeCommandHandler : IRequestHandler<AddEditAudienceTypeCommand, int>
{
    private readonly IStringLocalizer<AddEditAudienceTypeCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditAudienceTypeCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditAudienceTypeCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditAudienceTypeCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var audienceType = command.Adapt<AudienceType>();
            await _unitOfWork.Repository<AudienceType>().AddAsync(audienceType);
            await _unitOfWork.Commit(cancellationToken);
            return audienceType.Id;
        }
        else
        {
            var audienceType = await _unitOfWork.Repository<AudienceType>().GetByIdAsync(command.Id);

            if (audienceType is null)
            {
                throw new NotFoundException("Audience Type Not Found!");
            }

            audienceType.Name = command.Name;
            await _unitOfWork.Repository<AudienceType>().UpdateAsync(audienceType);
            await _unitOfWork.Commit(cancellationToken);
            return audienceType.Id;
        }
    }
}
