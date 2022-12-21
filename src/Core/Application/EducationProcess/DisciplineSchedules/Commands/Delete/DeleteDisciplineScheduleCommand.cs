using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Commands.Delete;

public class DeleteDisciplineScheduleCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteDisciplineScheduleCommandHandler : IRequestHandler<DeleteDisciplineScheduleCommand, int>
{
    private readonly IStringLocalizer<DeleteDisciplineScheduleCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteDisciplineScheduleCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteDisciplineScheduleCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteDisciplineScheduleCommand command, CancellationToken cancellationToken)
    {
        var disciplineSchedule = await _unitOfWork.Repository<DisciplineSchedule>().GetByIdAsync(command.Id);

        if (disciplineSchedule is null)
        {
            throw new NotFoundException("Discipline Schedule Not Found!");
        }

        await _unitOfWork.Repository<DisciplineSchedule>().DeleteAsync(disciplineSchedule);
        await _unitOfWork.Commit(cancellationToken);
        return disciplineSchedule.Id;
    }
}
