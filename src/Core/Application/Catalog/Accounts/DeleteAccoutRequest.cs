using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Accounts;
public class DeleteAccountRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteAccountRequest(Guid id) => Id = id;
}

public class DeleteAccountRequestHandler : IRequestHandler<DeleteAccountRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Account> _repository;
    private readonly IStringLocalizer _t;

    public DeleteAccountRequestHandler(IRepositoryWithEvents<Account> repository, IStringLocalizer<DeleteAccountRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Result<Guid>> Handle(DeleteAccountRequest request, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = account ?? throw new NotFoundException(_t["Account {0} Not Found.", request.Id]);

        await _repository.DeleteAsync(account, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}
