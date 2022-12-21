using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Commands.Delete;

public class DeleteIntermediateCertificationFormCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteIntermediateCertificationFormCommandHandler : IRequestHandler<DeleteIntermediateCertificationFormCommand, int>
{
    private readonly IStringLocalizer<DeleteIntermediateCertificationFormCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteIntermediateCertificationFormCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteIntermediateCertificationFormCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteIntermediateCertificationFormCommand command, CancellationToken cancellationToken)
    {
        var intermediateCertificationForm = await _unitOfWork.Repository<IntermediateCertificationForm>().GetByIdAsync(command.Id);

        if (intermediateCertificationForm is null)
        {
            throw new NotFoundException("Intermediate Certification Form Not Found!");
        }

        await _unitOfWork.Repository<IntermediateCertificationForm>().DeleteAsync(intermediateCertificationForm);
        await _unitOfWork.Commit(cancellationToken);
        return intermediateCertificationForm.Id;
    }
}
