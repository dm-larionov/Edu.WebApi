using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Audiences.Commands.Delete;

public class DeleteAudienceCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteAudienceCommandHandler : IRequestHandler<DeleteAudienceCommand, int>
{
    private readonly IStringLocalizer<DeleteAudienceCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteAudienceCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteAudienceCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteAudienceCommand command, CancellationToken cancellationToken)
    {
        var audience = await _unitOfWork.Repository<Audience>().GetByIdAsync(command.Id);

        if (audience is null)
        {
            throw new NotFoundException("Audience Not Found!");
        }

        await _unitOfWork.Repository<Audience>().DeleteAsync(audience);
        await _unitOfWork.Commit(cancellationToken);
        return audience.Id;
    }
}
