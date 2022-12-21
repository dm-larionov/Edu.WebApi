using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationModules.Commands.Delete;

public class DeleteEducationModuleCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteEducationModuleCommandHandler : IRequestHandler<DeleteEducationModuleCommand, int>
{
    private readonly IStringLocalizer<DeleteEducationModuleCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteEducationModuleCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteEducationModuleCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteEducationModuleCommand command, CancellationToken cancellationToken)
    {
        var educationModule = await _unitOfWork.Repository<EducationModule>().GetByIdAsync(command.Id);

        if (educationModule is null)
        {
            throw new NotFoundException("Education Module Not Found!");
        }

        await _unitOfWork.Repository<EducationModule>().DeleteAsync(educationModule);
        await _unitOfWork.Commit(cancellationToken);
        return educationModule.Id;
    }
}
