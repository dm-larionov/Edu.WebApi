using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Audiences.Commands.AddEdit;

public partial class AddEditAudienceCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string Number { get; set; }
    public int? EmployeeHeadId { get; set; }
    public int? AudienceTypeId { get; set; }
    public short? Capacity { get; set; }
}

internal class AddEditAudienceCommandHandler : IRequestHandler<AddEditAudienceCommand, int>
{
    private readonly IStringLocalizer<AddEditAudienceCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditAudienceCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditAudienceCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditAudienceCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var audience = command.Adapt<Audience>();
            await _unitOfWork.Repository<Audience>().AddAsync(audience);
            await _unitOfWork.Commit(cancellationToken);
            return audience.Id;
        }
        else
        {
            var audience = await _unitOfWork.Repository<Audience>().GetByIdAsync(command.Id);

            if (audience is null)
            {
                throw new NotFoundException("Audience Not Found!");
            }

            audience.Number = command.Number;
            audience.EmployeeHeadId = command.EmployeeHeadId;
            audience.AudienceTypeId = command.AudienceTypeId;
            audience.Capacity = command.Capacity;
            await _unitOfWork.Repository<Audience>().UpdateAsync(audience);
            await _unitOfWork.Commit(cancellationToken);
            return audience.Id;
        }
    }
}
