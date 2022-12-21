using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationLevels.Commands.Delete;

public class DeleteEducationLevelCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteEducationLevelCommandHandler : IRequestHandler<DeleteEducationLevelCommand, int>
{
    private readonly IStringLocalizer<DeleteEducationLevelCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteEducationLevelCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteEducationLevelCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteEducationLevelCommand command, CancellationToken cancellationToken)
    {
        var educationLevel = await _unitOfWork.Repository<EducationLevel>().GetByIdAsync(command.Id);

        if (educationLevel is null)
        {
            throw new NotFoundException("Education Level Not Found!");
        }

        await _unitOfWork.Repository<EducationLevel>().DeleteAsync(educationLevel);
        await _unitOfWork.Commit(cancellationToken);
        return educationLevel.Id;
    }
}
