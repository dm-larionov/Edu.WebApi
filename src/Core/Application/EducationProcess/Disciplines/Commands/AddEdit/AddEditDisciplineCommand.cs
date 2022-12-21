using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Disciplines.Commands.AddEdit;

public partial class AddEditDisciplineCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string DisciplineIndex { get; set; } = default!;
    public int? CathedraId { get; set; }
    [Required]
    public int EducationCycleId { get; set; }
    public int? EducationModuleId { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

internal class AddEditDisciplineCommandHandler : IRequestHandler<AddEditDisciplineCommand, int>
{
    private readonly IStringLocalizer<AddEditDisciplineCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditDisciplineCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditDisciplineCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditDisciplineCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var discipline = command.Adapt<Discipline>();
            await _unitOfWork.Repository<Discipline>().AddAsync(discipline);
            await _unitOfWork.Commit(cancellationToken);
            return discipline.Id;
        }
        else
        {
            var discipline = await _unitOfWork.Repository<Discipline>().GetByIdAsync(command.Id);

            if (discipline is null)
            {
                throw new NotFoundException("Discipline Not Found!");
            }

            discipline.DisciplineIndex = command.DisciplineIndex;
            discipline.CathedraId = command.CathedraId;
            discipline.EducationCycleId = command.EducationCycleId;
            discipline.EducationModuleId = command.EducationModuleId;
            discipline.Name = command.Name;
            discipline.Description = command.Description;
            await _unitOfWork.Repository<Discipline>().UpdateAsync(discipline);
            await _unitOfWork.Commit(cancellationToken);
            return discipline.Id;
        }
    }
}
