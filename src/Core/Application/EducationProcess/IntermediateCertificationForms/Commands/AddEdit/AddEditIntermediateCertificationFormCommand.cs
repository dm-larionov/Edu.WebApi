using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Commands.AddEdit;

public partial class AddEditIntermediateCertificationFormCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
}

internal class AddEditIntermediateCertificationFormCommandHandler : IRequestHandler<AddEditIntermediateCertificationFormCommand, int>
{
    private readonly IStringLocalizer<AddEditIntermediateCertificationFormCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditIntermediateCertificationFormCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditIntermediateCertificationFormCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditIntermediateCertificationFormCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var intermediateCertificationForm = command.Adapt<IntermediateCertificationForm>();
            await _unitOfWork.Repository<IntermediateCertificationForm>().AddAsync(intermediateCertificationForm);
            await _unitOfWork.Commit(cancellationToken);
            return intermediateCertificationForm.Id;
        }
        else
        {
            var intermediateCertificationForm = await _unitOfWork.Repository<IntermediateCertificationForm>().GetByIdAsync(command.Id);

            if (intermediateCertificationForm is null)
            {
                throw new NotFoundException("Intermediate Certification Form Not Found!");
            }

            intermediateCertificationForm.Name = command.Name;
            await _unitOfWork.Repository<IntermediateCertificationForm>().UpdateAsync(intermediateCertificationForm);
            await _unitOfWork.Commit(cancellationToken);
            return intermediateCertificationForm.Id;
        }
    }
}
