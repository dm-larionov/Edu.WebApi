using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplines.Commands.AddEdit;

public partial class AddEditFixedDisciplineCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public int FixingEmployeeId { get; set; }
    [Required]
    public int DisciplineSemesterId { get; set; }
    [Required]
    public int StudentGroupId { get; set; }
    [Required]
    public int FixedDisciplineStatusId { get; set; }
}

internal class AddEditFixedDisciplineCommandHandler : IRequestHandler<AddEditFixedDisciplineCommand, int>
{
    private readonly IStringLocalizer<AddEditFixedDisciplineCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditFixedDisciplineCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditFixedDisciplineCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditFixedDisciplineCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var fixedDiscipline = command.Adapt<FixedDiscipline>();
            await _unitOfWork.Repository<FixedDiscipline>().AddAsync(fixedDiscipline);
            await _unitOfWork.Commit(cancellationToken);
            return fixedDiscipline.Id;
        }
        else
        {
            var fixedDiscipline = await _unitOfWork.Repository<FixedDiscipline>().GetByIdAsync(command.Id);

            if (fixedDiscipline is null)
            {
                throw new NotFoundException("Fixed Discipline Not Found!");
            }

            fixedDiscipline.FixingEmployeeId = command.FixingEmployeeId;
            fixedDiscipline.DisciplineSemesterId = command.DisciplineSemesterId;
            fixedDiscipline.StudentGroupId = command.StudentGroupId;
            fixedDiscipline.FixedDisciplineStatusId = command.FixedDisciplineStatusId;

            await _unitOfWork.Repository<FixedDiscipline>().UpdateAsync(fixedDiscipline);
            await _unitOfWork.Commit(cancellationToken);
            return fixedDiscipline.Id;
        }
    }
}
