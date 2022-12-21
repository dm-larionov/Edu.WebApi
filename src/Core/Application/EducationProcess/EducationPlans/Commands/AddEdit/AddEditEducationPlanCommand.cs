using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Commands.AddEdit;

public partial class AddEditEducationPlanCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public int FsesCategoryPartitionId { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public short BeginingYear { get; set; }
    [Required]
    public short EndingYear { get; set; }
    public string? Description { get; set; }
}

internal class AddEditEducationPlanCommandHandler : IRequestHandler<AddEditEducationPlanCommand, int>
{
    private readonly IStringLocalizer<AddEditEducationPlanCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditEducationPlanCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditEducationPlanCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditEducationPlanCommand command, CancellationToken cancellationToken)
    {
        var fsesCategoryPartition =
            await _unitOfWork.Repository<FsesCategoryPartition>().GetByIdAsync(command.FsesCategoryPartitionId);

        if (fsesCategoryPartition == null)
        {
            throw new NotFoundException("Fses Category Partition was not found");
        }

        if (command.Id == 0)
        {
            var educationPlan = command.Adapt<EducationPlan>();
            await _unitOfWork.Repository<EducationPlan>().AddAsync(educationPlan);
            await _unitOfWork.Commit(cancellationToken);
            return educationPlan.Id;
        }
        else
        {
            var educationPlan = await _unitOfWork.Repository<EducationPlan>().GetByIdAsync(command.Id);

            if (educationPlan is null)
            {
                throw new NotFoundException("Education Plan not found");
            }

            if (command.FsesCategoryPartitionId != educationPlan.FsesCategoryPartitionId)
            {
                /* Get any student group that has current education plan */
                var isAnyStudentGroup = await _unitOfWork.Repository<StudentGroup>().Entities
                    .AnyAsync(
                        x => x.EducationPlanId == educationPlan.Id &&
                             x.ReceivedEducation.ReceivedSpecialty.FsesCategoryPartitionId !=
                             command.FsesCategoryPartitionId,
                        cancellationToken);

                if (isAnyStudentGroup)
                {
                    throw new ConflictException("Education Plan has student group");
                }
            }

            if (command.BeginingYear != educationPlan.BeginingYear || command.EndingYear != educationPlan.EndingYear)
            {
                /* Get any student group that has current education plan */
                var isAnyStudentGroup = await _unitOfWork.Repository<StudentGroup>().Entities
                    .AnyAsync(x => x.EducationPlanId == educationPlan.Id, cancellationToken);

                if (isAnyStudentGroup)
                {
                    throw new ConflictException("Education Plan has student group");
                }
            }

            educationPlan.FsesCategoryPartitionId = command.FsesCategoryPartitionId;
            educationPlan.Name = command.Name;
            educationPlan.BeginingYear = command.BeginingYear;
            educationPlan.EndingYear = command.EndingYear;
            educationPlan.Description = command.Description;
            await _unitOfWork.Repository<EducationPlan>().UpdateAsync(educationPlan);
            await _unitOfWork.Commit(cancellationToken);
            return educationPlan.Id;
        }
    }
}
