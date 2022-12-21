using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Commands.Delete;

public class DeleteEducationPlanCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteEducationPlanCommandHandler : IRequestHandler<DeleteEducationPlanCommand, int>
{
    private readonly IStringLocalizer<DeleteEducationPlanCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteEducationPlanCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteEducationPlanCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteEducationPlanCommand command, CancellationToken cancellationToken)
    {

        var educationPlan = await _unitOfWork.Repository<EducationPlan>().GetByIdAsync(command.Id);
        if (educationPlan != null)
        {
            var isEducationPlanUsed = await _unitOfWork.Repository<StudentGroup>().Entities
                .AnyAsync(x => x.EducationPlanId == educationPlan.Id);
            await _unitOfWork.Repository<EducationPlan>().DeleteAsync(educationPlan);

            if (isEducationPlanUsed)
            {
                throw new ConflictException("Education Plan is using by student group");
            }

            await _unitOfWork.Commit(cancellationToken);
            return educationPlan.Id;
        }

        throw new NotFoundException("Education Plan Not Found");
    }
}
