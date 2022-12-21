using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationCycles.Commands.AddEdit;

public partial class AddEditEducationCycleCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string EducationCycleIndex { get; set; } = default!;
    [Required]
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

internal class AddEditEducationCyclesAndModuleCommandHandler : IRequestHandler<AddEditEducationCycleCommand, int>
{
    private readonly IStringLocalizer<AddEditEducationCyclesAndModuleCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditEducationCyclesAndModuleCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditEducationCyclesAndModuleCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditEducationCycleCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var educationCyclesAndModule = command.Adapt<EducationCycle>();
            await _unitOfWork.Repository<EducationCycle>().AddAsync(educationCyclesAndModule);
            await _unitOfWork.Commit(cancellationToken);
            return educationCyclesAndModule.Id;
        }
        else
        {
            var educationCyclesAndModule = await _unitOfWork.Repository<EducationCycle>().GetByIdAsync(command.Id);

            if (educationCyclesAndModule is null)
            {
                throw new NotFoundException("Education Cycle Not Found!");
            }

            educationCyclesAndModule.EducationCycleIndex = command.EducationCycleIndex;
            educationCyclesAndModule.Name = command.Name;
            educationCyclesAndModule.Description = command.Description;
            await _unitOfWork.Repository<EducationCycle>().UpdateAsync(educationCyclesAndModule);
            await _unitOfWork.Commit(cancellationToken);
            return educationCyclesAndModule.Id;
        }
    }
}
