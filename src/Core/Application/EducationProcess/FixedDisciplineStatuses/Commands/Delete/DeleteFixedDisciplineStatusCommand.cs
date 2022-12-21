using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Commands.Delete;

public class DeleteFixedDisciplineStatusCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteFixedDisciplineStatusCommandHandler : IRequestHandler<DeleteFixedDisciplineStatusCommand, int>
{
    private readonly IStringLocalizer<DeleteFixedDisciplineStatusCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteFixedDisciplineStatusCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteFixedDisciplineStatusCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteFixedDisciplineStatusCommand command, CancellationToken cancellationToken)
    {
        var fixedDisciplineStatus = await _unitOfWork.Repository<FixedDisciplineStatus>().GetByIdAsync(command.Id);

        if (fixedDisciplineStatus is null)
        {
            throw new NotFoundException("Fixed Discipline Status Not Found!");
        }

        await _unitOfWork.Repository<FixedDisciplineStatus>().DeleteAsync(fixedDisciplineStatus);
        await _unitOfWork.Commit(cancellationToken);
        return fixedDisciplineStatus.Id;
    }
}
