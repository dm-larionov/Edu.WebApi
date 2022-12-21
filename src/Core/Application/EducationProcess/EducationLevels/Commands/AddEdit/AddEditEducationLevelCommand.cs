using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationLevels.Commands.AddEdit;

public partial class AddEditEducationLevelCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
}

internal class AddEditEducationLevelCommandHandler : IRequestHandler<AddEditEducationLevelCommand, int>
{
    private readonly IStringLocalizer<AddEditEducationLevelCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditEducationLevelCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditEducationLevelCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditEducationLevelCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var educationLevel = command.Adapt<EducationLevel>();
            await _unitOfWork.Repository<EducationLevel>().AddAsync(educationLevel);
            await _unitOfWork.Commit(cancellationToken);
            return educationLevel.Id;
        }
        else
        {
            var educationLevel = await _unitOfWork.Repository<EducationLevel>().GetByIdAsync(command.Id);

            if (educationLevel is null)
            {
                throw new NotFoundException("Education Level Not Found!");
            }

            educationLevel.Name = command.Name;
            await _unitOfWork.Repository<EducationLevel>().UpdateAsync(educationLevel);
            await _unitOfWork.Commit(cancellationToken);
            return educationLevel.Id;
        }
    }
}
