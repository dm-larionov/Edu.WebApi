using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Commands.Delete;

public class DeleteFsesCategoryPartitionCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteFsesCategoryPartitionCommandHandler : IRequestHandler<DeleteFsesCategoryPartitionCommand, int>
{
    private readonly IStringLocalizer<DeleteFsesCategoryPartitionCommandHandler> _localizer;
    private readonly IRepositoryAud<FsesCategoryPartition> _repository;

    public DeleteFsesCategoryPartitionCommandHandler(IRepositoryAud<FsesCategoryPartition> repository, IStringLocalizer<DeleteFsesCategoryPartitionCommandHandler> localizer)
    {
        _repository = repository;
        _localizer = localizer;
    }

    public async Task<int> Handle(DeleteFsesCategoryPartitionCommand command, CancellationToken cancellationToken)
    {
        var fsesCategoryPartition = await _repository.GetByIdAsync(command.Id, cancellationToken);

        _ = fsesCategoryPartition ?? throw new NotFoundException("NotFound");

        await _repository.DeleteAsync(fsesCategoryPartition, cancellationToken);

        return command.Id;
    }
}
