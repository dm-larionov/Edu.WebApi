using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Posts.Commands.AddEdit;

public partial class AddEditPostCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
}

internal class AddEditPostCommandHandler : IRequestHandler<AddEditPostCommand, int>
{
    private readonly IStringLocalizer<AddEditPostCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditPostCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditPostCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditPostCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var post = command.Adapt<Post>();
            await _unitOfWork.Repository<Post>().AddAsync(post);
            await _unitOfWork.Commit(cancellationToken);
            return post.Id;
        }
        else
        {
            var post = await _unitOfWork.Repository<Post>().GetByIdAsync(command.Id);

            if (post is null)
            {
                throw new NotFoundException("Post Not Found!");
            }

            post.Name = command.Name;
            await _unitOfWork.Repository<Post>().UpdateAsync(post);
            await _unitOfWork.Commit(cancellationToken);
            return post.Id;
        }
    }
}
