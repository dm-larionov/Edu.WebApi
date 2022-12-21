using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.EducationCycles.Commands.Delete;

public class DeleteEducationCycleCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteEducationCycleCommandHandler : IRequestHandler<DeleteEducationCycleCommand, int>
{
    private readonly IStringLocalizer<DeleteEducationCycleCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteEducationCycleCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteEducationCycleCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteEducationCycleCommand command, CancellationToken cancellationToken)
    {
        var educationCycle = await _unitOfWork.Repository<EducationCycle>().GetByIdAsync(command.Id);

        if (educationCycle is null)
        {
            throw new NotFoundException("Education Cycle Not Found!");
        }

        await _unitOfWork.Repository<EducationCycle>().DeleteAsync(educationCycle);
        await _unitOfWork.Commit(cancellationToken);
        return educationCycle.Id;
    }
}
