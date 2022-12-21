using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationModules.Commands.AddEdit;

public partial class AddEditEducationModuleCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string EducationModuleIndex { get; set; } = default!;
    [Required]
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

internal class AddEditEducationModuleCommandHandler : IRequestHandler<AddEditEducationModuleCommand, int>
{
    private readonly IStringLocalizer<AddEditEducationModuleCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditEducationModuleCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditEducationModuleCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditEducationModuleCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var educationModule = command.Adapt<EducationModule>();
            await _unitOfWork.Repository<EducationModule>().AddAsync(educationModule);
            await _unitOfWork.Commit(cancellationToken);
            return educationModule.Id;
        }
        else
        {
            var educationModule = await _unitOfWork.Repository<EducationModule>().GetByIdAsync(command.Id);

            if (educationModule is null)
            {
                throw new NotFoundException("Education Module Not Found!");
            }

            educationModule.EducationModuleIndex = command.EducationModuleIndex;
            educationModule.Name = command.Name;
            educationModule.Description = command.Description;
            await _unitOfWork.Repository<EducationModule>().UpdateAsync(educationModule);
            await _unitOfWork.Commit(cancellationToken);
            return educationModule.Id;
        }
    }
}
