using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Commands.Delete;

public class DeleteDisciplineScheduleReplacementCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteDisciplineScheduleReplacementCommandHandler : IRequestHandler<DeleteDisciplineScheduleReplacementCommand, int>
{
    private readonly IStringLocalizer<DeleteDisciplineScheduleReplacementCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteDisciplineScheduleReplacementCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteDisciplineScheduleReplacementCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteDisciplineScheduleReplacementCommand command, CancellationToken cancellationToken)
    {
        var disciplineScheduleReplacement = await _unitOfWork.Repository<DisciplineScheduleReplacement>().GetByIdAsync(command.Id);

        if (disciplineScheduleReplacement is null)
        {
            throw new NotFoundException("Discipline Schedule Replacement Not Found!");
        }

        await _unitOfWork.Repository<DisciplineScheduleReplacement>().DeleteAsync(disciplineScheduleReplacement);
        await _unitOfWork.Commit(cancellationToken);
        return disciplineScheduleReplacement.Id;
    }
}
