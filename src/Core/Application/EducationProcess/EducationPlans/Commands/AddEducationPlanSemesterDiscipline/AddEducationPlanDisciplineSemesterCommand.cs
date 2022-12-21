using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Commands.AddEducationPlanSemesterDiscipline;

public partial class AddEducationPlanDisciplineSemesterCommand : IRequest<int>
{
    [Required]
    public int EducationPlanId { get; set; }
    [Required]
    public int DisciplineSemesterId { get; set; }
}

internal class AddEducationPlanDisciplineSemesterCommandHandler : IRequestHandler<AddEducationPlanDisciplineSemesterCommand, int>
{
    private readonly IStringLocalizer<AddEducationPlanDisciplineSemesterCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEducationPlanDisciplineSemesterCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEducationPlanDisciplineSemesterCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEducationPlanDisciplineSemesterCommand command, CancellationToken cancellationToken)
    {
        var educationPlan =
            await _unitOfWork.Repository<EducationPlan>().GetByIdAsync(command.EducationPlanId);

        var disciplineSemester =
            await _unitOfWork.Repository<DisciplineSemester>().GetByIdAsync(command.DisciplineSemesterId);

        if (educationPlan == null)
        {
            throw new NotFoundException("Education Plan was not found");
        }

        if (disciplineSemester == null)
        {
            throw new NotFoundException("Discipline Semester was not found");
        }

        bool isSameFsesCategory = await _unitOfWork.Repository<DisciplineSemester>().Entities
            .Where(x => x.Id == command.DisciplineSemesterId)
            .AnyAsync(
                x => x.Discipline.Cathedra.FsesCategoryPartitions.Any(
                    x => x.Id == educationPlan.FsesCategoryPartitionId),
                cancellationToken: cancellationToken);

        if (isSameFsesCategory == false)
        {
            throw new ConflictException("Discipline Semester incorrect for current education plan");
        }

        educationPlan.SemesterDisciplines.Add(disciplineSemester);

        await _unitOfWork.Repository<EducationPlan>().UpdateAsync(educationPlan);
        await _unitOfWork.Commit(cancellationToken);
        return educationPlan.Id;
    }
}
