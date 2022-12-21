using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.AudienceTypes.Commands.Delete;

public class DeleteAudienceTypeCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteAudienceTypeCommandHandler : IRequestHandler<DeleteAudienceTypeCommand, int>
{
    private readonly IStringLocalizer<DeleteAudienceTypeCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeleteAudienceTypeCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteAudienceTypeCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteAudienceTypeCommand command, CancellationToken cancellationToken)
    {
        var audienceType = await _unitOfWork.Repository<AudienceType>().GetByIdAsync(command.Id);

        if (audienceType is null)
        {
            throw new NotFoundException("Audience Type Not Found!");
        }

        await _unitOfWork.Repository<AudienceType>().DeleteAsync(audienceType);
        await _unitOfWork.Commit(cancellationToken);
        return audienceType.Id;
    }
}
