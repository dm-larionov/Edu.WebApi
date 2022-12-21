using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Posts.Queries.GetById;

public class GetPostByIdQuery : IRequest<GetPostByIdResponse>
{
    public int Id { get; set; }
}

internal class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, GetPostByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetPostByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetPostByIdResponse> Handle(GetPostByIdQuery query, CancellationToken cancellationToken)
    {
        var post = await _unitOfWork.Repository<Post>().GetByIdAsync(query.Id);
        return post.Adapt<GetPostByIdResponse>();
    }
}
