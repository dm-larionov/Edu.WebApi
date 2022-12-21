using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Disciplines.Commands.Delete;

public class DeleteDisciplineCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteDisciplineCommandHandler : IRequestHandler<DeleteDisciplineCommand, int>
{
    private readonly IStringLocalizer<DeleteDisciplineCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteDisciplineCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteDisciplineCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteDisciplineCommand command, CancellationToken cancellationToken)
    {
        var discipline = await _unitOfWork.Repository<Discipline>().GetByIdAsync(command.Id);

        if (discipline is null)
        {
            throw new NotFoundException("Discipline Not Found!");
        }

        await _unitOfWork.Repository<Discipline>().DeleteAsync(discipline);
        await _unitOfWork.Commit(cancellationToken);
        return discipline.Id;
    }
}
