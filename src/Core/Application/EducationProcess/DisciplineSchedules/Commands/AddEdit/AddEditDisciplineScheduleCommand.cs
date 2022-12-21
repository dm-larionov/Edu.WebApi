using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Commands.AddEdit;

public partial class AddEditDisciplineScheduleCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public int FixedDisciplineId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int PairNumber { get; set; }
    public int? AudienceId { get; set; }
    public bool? IsEvenPair { get; set; }
    public bool? IsFirstSubgroup { get; set; }
}

internal class AddEditDisciplineScheduleCommandHandler : IRequestHandler<AddEditDisciplineScheduleCommand, int>
{
    private readonly IStringLocalizer<AddEditDisciplineScheduleCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditDisciplineScheduleCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditDisciplineScheduleCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditDisciplineScheduleCommand command, CancellationToken cancellationToken)
    {
        var fixedDisciplineId =
            await _unitOfWork.Repository<FixedDiscipline>().GetByIdAsync(command.FixedDisciplineId);

        if (fixedDisciplineId == null)
        {
            throw new NotFoundException("Fixed Discipline was not found");
        }

        if (command.AudienceId != null)
        {
            var academicYear =
                await _unitOfWork.Repository<Audience>().GetByIdAsync((int)command.AudienceId);

            if (academicYear == null)
            {
                throw new NotFoundException("Audience was not found");
            }
        }


        if (command.Id == 0)
        {
            var disciplineSchedule = command.Adapt<DisciplineSchedule>();
            await _unitOfWork.Repository<DisciplineSchedule>().AddAsync(disciplineSchedule);
            await _unitOfWork.Commit(cancellationToken);
            return disciplineSchedule.Id;
        }
        else
        {
            var disciplineSchedule = await _unitOfWork.Repository<DisciplineSchedule>().GetByIdAsync(command.Id);

            if (disciplineSchedule is null)
            {
                throw new NotFoundException("Discipline Schedule was not found");
            }

            disciplineSchedule.FixedDisciplineId = command.FixedDisciplineId;
            disciplineSchedule.Date = command.Date;
            disciplineSchedule.PairNumber = command.PairNumber;
            disciplineSchedule.AudienceId = command.AudienceId;
            disciplineSchedule.IsEvenPair = command.IsEvenPair;
            disciplineSchedule.IsFirstSubgroup = command.IsFirstSubgroup;
            await _unitOfWork.Repository<DisciplineSchedule>().UpdateAsync(disciplineSchedule);
            await _unitOfWork.Commit(cancellationToken);
            return disciplineSchedule.Id;
        }
    }
}
