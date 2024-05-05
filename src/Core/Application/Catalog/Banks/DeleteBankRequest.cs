namespace TD.WebApi.Application.Catalog.Banks;

public class DeleteBankRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteBankRequest(Guid id) => Id = id;
}

public class DeleteBankRequestHandler : IRequestHandler<DeleteBankRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Bank> _repository;
    private readonly IStringLocalizer _t;

    public DeleteBankRequestHandler(IRepositoryWithEvents<Bank> repository, IStringLocalizer<DeleteBankRequestHandler> localizer) =>
        (_repository,  _t) = (repository,  localizer);

    public async Task<Result<Guid>> Handle(DeleteBankRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_t["Bank {0} Not Found."]);

        await _repository.DeleteAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}