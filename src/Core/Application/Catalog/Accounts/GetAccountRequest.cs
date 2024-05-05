using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Accounts;
public class GetAccountRequest : IRequest<Result<AccountDetailsDto>>
{
    public Guid Id { get; set; }

    public GetAccountRequest(Guid id) => Id = id;
}

public class GetAccountRequestHandler : IRequestHandler<GetAccountRequest, Result<AccountDetailsDto>>
{
    private readonly IDapperRepository _repository;
    private readonly IStringLocalizer _t;

    public GetAccountRequestHandler(IDapperRepository repository, IStringLocalizer<GetAccountRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<Result<AccountDetailsDto>> Handle(GetAccountRequest request, CancellationToken cancellationToken)
    {
        string sql = @"
            SELECT 
                Accounts.*,
                Banks.Name AS BankName 
            FROM 
                [Catalog].[Accounts] Accounts
                LEFT JOIN [Catalog].[Banks] Banks ON Accounts.BankId = Banks.Id ";

        var itemResult = await _repository.QueryFirstOrDefaultObjectAsync<AccountDetailsDto>(
            $"{sql} WHERE Accounts.Id = '{request.Id}' AND Accounts.DeletedOn IS NULL AND Accounts.TenantId = '@tenant'", cancellationToken: cancellationToken);

        _ = itemResult ?? throw new NotFoundException(_t["Accounts {0} Not Found.", request.Id]);

        return Result<AccountDetailsDto>.Success(itemResult);
    }
}
