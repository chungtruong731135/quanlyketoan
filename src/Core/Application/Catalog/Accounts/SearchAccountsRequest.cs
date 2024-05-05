using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Accounts;
public class SearchAccountRequest : PaginationFilter, IRequest<PaginationResponse<AccountDto>>
{
}

public class SearchAccountRequestHandler : IRequestHandler<SearchAccountRequest, PaginationResponse<AccountDto>>
{
    private readonly IDapperRepository _repository;

    public SearchAccountRequestHandler(IDapperRepository repository) => _repository = repository;

    public async Task<PaginationResponse<AccountDto>> Handle(SearchAccountRequest request, CancellationToken cancellationToken)
    {
        string query = @"
            SELECT 
                Accounts.*,
                Banks.Name AS BankName 
            FROM 
                [Catalog].[Accounts] Accounts
                LEFT JOIN [Catalog].[Banks] Banks ON Accounts.BankId = Banks.Id ";

        string where = " ";

        if (!string.IsNullOrEmpty(request.Keyword))
        {
            where += $" AND (Accounts.Name LIKE '%{request.Keyword}%' OR  Accounts.Code LIKE N'%{request.Keyword}%' ) ";
        }

        where = " WHERE Accounts.DeletedOn IS NULL AND Accounts.TenantId = '@tenant' " + where;

        string whereOrder = " ORDER BY Main.CreatedOn DESC ";

        string paging = $" OFFSET {(request.PageNumber - 1) * request.PageSize} ROWS FETCH NEXT {request.PageSize} ROWS ONLY";

        string sql = $"WITH Main AS ({query} {where} ), TotalCount AS (SELECT COUNT(Id) AS [TotalCount]  FROM Main) SELECT * FROM Main, TotalCount {whereOrder} {paging} ";

        return await _repository.PaginatedListNewAsync<AccountDto>(sql, request.PageNumber, request.PageSize, cancellationToken);
    }
}
