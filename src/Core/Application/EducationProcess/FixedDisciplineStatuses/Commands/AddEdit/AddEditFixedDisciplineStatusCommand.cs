using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Commands.AddEdit;

public partial class AddEditFixedDisciplineStatusCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

internal class AddEditFixedDisciplineStatusCommandHandler : IRequestHandler<AddEditFixedDisciplineStatusCommand, int>
{
    private readonly IStringLocalizer<AddEditFixedDisciplineStatusCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditFixedDisciplineStatusCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditFixedDisciplineStatusCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditFixedDisciplineStatusCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var fixedDisciplineStatus = command.Adapt<FixedDisciplineStatus>();
            await _unitOfWork.Repository<FixedDisciplineStatus>().AddAsync(fixedDisciplineStatus);
            await _unitOfWork.Commit(cancellationToken);
            return fixedDisciplineStatus.Id;
        }
        else
        {
            var fixedDisciplineStatus = await _unitOfWork.Repository<FixedDisciplineStatus>().GetByIdAsync(command.Id);

            if (fixedDisciplineStatus is null)
            {
                throw new NotFoundException("Fixed Discipline Status Not Found!");
            }

            fixedDisciplineStatus.Name = command.Name;
            fixedDisciplineStatus.Description = command.Description;

            await _unitOfWork.Repository<FixedDisciplineStatus>().UpdateAsync(fixedDisciplineStatus);
            await _unitOfWork.Commit(cancellationToken);
            return fixedDisciplineStatus.Id;
        }
    }
}
