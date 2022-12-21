using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationForms.Commands.Delete;

public class DeleteEducationFormCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteEducationFormCommandHandler : IRequestHandler<DeleteEducationFormCommand, int>
{
    private readonly IStringLocalizer<DeleteEducationFormCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteEducationFormCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteEducationFormCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteEducationFormCommand command, CancellationToken cancellationToken)
    {
        var educationForm = await _unitOfWork.Repository<EducationForm>().GetByIdAsync(command.Id);

        if (educationForm is null)
        {
            throw new NotFoundException("Education Form Not Found!");
        }

        await _unitOfWork.Repository<EducationForm>().DeleteAsync(educationForm);
        await _unitOfWork.Commit(cancellationToken);
        return educationForm.Id;
    }
}
