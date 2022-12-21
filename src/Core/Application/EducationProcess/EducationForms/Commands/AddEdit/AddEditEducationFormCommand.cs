using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationForms.Commands.AddEdit;

public partial class AddEditEducationFormCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
}

internal class AddEditEducationFormCommandHandler : IRequestHandler<AddEditEducationFormCommand, int>
{
    private readonly IStringLocalizer<AddEditEducationFormCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditEducationFormCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditEducationFormCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditEducationFormCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var educationForm = command.Adapt<EducationForm>();
            await _unitOfWork.Repository<EducationForm>().AddAsync(educationForm);
            await _unitOfWork.Commit(cancellationToken);
            return educationForm.Id;
        }
        else
        {
            var educationForm = await _unitOfWork.Repository<EducationForm>().GetByIdAsync(command.Id);

            if (educationForm is null)
            {
                throw new NotFoundException("Education Form Not Found!");
            }

            educationForm.Name = command.Name;
            await _unitOfWork.Repository<EducationForm>().UpdateAsync(educationForm);
            await _unitOfWork.Commit(cancellationToken);
            return educationForm.Id;
        }
    }
}
