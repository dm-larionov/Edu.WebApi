using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Cathedras.Commands.Delete;

public class DeleteCathedraCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteCathedraCommandHandler : IRequestHandler<DeleteCathedraCommand, int>
{
    private readonly IStringLocalizer<DeleteCathedraCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteCathedraCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteCathedraCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteCathedraCommand command, CancellationToken cancellationToken)
    {
        var cathedra = await _unitOfWork.Repository<Cathedra>().GetByIdAsync(command.Id);

        if (cathedra is null)
        {
            throw new NotFoundException("Cathedra Not Found!");
        }

        await _unitOfWork.Repository<Cathedra>().DeleteAsync(cathedra);
        await _unitOfWork.Commit(cancellationToken);
        return cathedra.Id;
    }
}
