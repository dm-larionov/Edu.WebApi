using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Commands.AddEdit;

public partial class AddEditDisciplineScheduleReplacementCommand : IRequest<int>
{
    public int Id { get; set; }
    public int? DisciplineScheduleId { get; set; }
    [Required]
    public int FixedDisciplineId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int PairNumber { get; set; }
    public int? AudienceId { get; set; }
    public bool? IsFirstSubgroup { get; set; }
}

internal class AddEditDisciplineScheduleReplacementCommandHandler : IRequestHandler<AddEditDisciplineScheduleReplacementCommand, int>
{
    private readonly IStringLocalizer<AddEditDisciplineScheduleReplacementCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditDisciplineScheduleReplacementCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditDisciplineScheduleReplacementCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditDisciplineScheduleReplacementCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var disciplineScheduleReplacement = command.Adapt<DisciplineScheduleReplacement>();
            await _unitOfWork.Repository<DisciplineScheduleReplacement>().AddAsync(disciplineScheduleReplacement);
            await _unitOfWork.Commit(cancellationToken);
            return disciplineScheduleReplacement.Id;
        }
        else
        {
            var disciplineScheduleReplacement = await _unitOfWork.Repository<DisciplineScheduleReplacement>().GetByIdAsync(command.Id);

            if (disciplineScheduleReplacement is null)
            {
                throw new NotFoundException("Discipline Schedule Replacement Not Found!");
            }

            disciplineScheduleReplacement.DisciplineScheduleId = command.DisciplineScheduleId;
            disciplineScheduleReplacement.FixedDisciplineId = command.FixedDisciplineId;
            disciplineScheduleReplacement.Date = command.Date;
            disciplineScheduleReplacement.PairNumber = command.PairNumber;
            disciplineScheduleReplacement.AudienceId = command.AudienceId;
            disciplineScheduleReplacement.IsFirstSubgroup = command.IsFirstSubgroup;

            await _unitOfWork.Repository<DisciplineScheduleReplacement>().UpdateAsync(disciplineScheduleReplacement);
            await _unitOfWork.Commit(cancellationToken);
            return disciplineScheduleReplacement.Id;
        }
    }
}
