using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplines.Commands.Delete;

public class DeleteFixedDisciplineCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteFixedDisciplineCommandHandler : IRequestHandler<DeleteFixedDisciplineCommand, int>
{
    private readonly IStringLocalizer<DeleteFixedDisciplineCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteFixedDisciplineCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteFixedDisciplineCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteFixedDisciplineCommand command, CancellationToken cancellationToken)
    {
        var fixedDiscipline = await _unitOfWork.Repository<FixedDiscipline>().GetByIdAsync(command.Id);

        if (fixedDiscipline is null)
        {
            throw new NotFoundException("Fixed Discipline Not Found!");
        }

        await _unitOfWork.Repository<FixedDiscipline>().DeleteAsync(fixedDiscipline);
        await _unitOfWork.Commit(cancellationToken);
        return fixedDiscipline.Id;
    }
}
