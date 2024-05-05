using Microsoft.AspNetCore.Mvc;
using TD.WebApi.Application.Catalog.Accounts;

namespace TD.WebApi.Host.Controllers.Catalog;
public class AccountsController : VersionedApiController
{
    [HttpPost("search")]
    [OpenApiOperation("Danh sách tài khoản.", "")]
    public Task<PaginationResponse<AccountDto>> SearchAsync(SearchAccountRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Chi tiết tài khoản.", "")]
    public Task<Result<AccountDetailsDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetAccountRequest(id));
    }

    [HttpPost]
    [OpenApiOperation("Tạo mới tài khoản.", "")]
    public Task<Result<Guid>> CreateAsync(CreateAccountRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Cập nhật tài khoản.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateAccountRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Xóa tài khoản.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteAccountRequest(id));
    }
}
