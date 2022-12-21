using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Commands.Delete;

public class DeleteDisciplineSemesterCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteDisciplineSemesterCommandHandler : IRequestHandler<DeleteDisciplineSemesterCommand, int>
{
    private readonly IStringLocalizer<DeleteDisciplineSemesterCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteDisciplineSemesterCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteDisciplineSemesterCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteDisciplineSemesterCommand command, CancellationToken cancellationToken)
    {
        var disciplineSemester = await _unitOfWork.Repository<DisciplineSemester>().GetByIdAsync(command.Id);

        if (disciplineSemester is null)
        {
            throw new NotFoundException("Discipline Semester Not Found!");
        }

        await _unitOfWork.Repository<DisciplineSemester>().DeleteAsync(disciplineSemester);
        await _unitOfWork.Commit(cancellationToken);
        return disciplineSemester.Id;
    }
}
