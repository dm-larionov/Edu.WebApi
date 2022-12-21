using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.Posts.Commands.Delete;

public class DeletePostCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, int>
{
    private readonly IStringLocalizer<DeletePostCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public DeletePostCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeletePostCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeletePostCommand command, CancellationToken cancellationToken)
    {
        var post = await _unitOfWork.Repository<Post>().GetByIdAsync(command.Id);

        if (post is null)
        {
            throw new NotFoundException("Post Not Found!");
        }

        await _unitOfWork.Repository<Post>().DeleteAsync(post);
        await _unitOfWork.Commit(cancellationToken);
        return post.Id;
    }
}
