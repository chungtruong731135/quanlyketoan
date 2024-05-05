using TD.WebApi.Application.Catalog.Banks;

namespace TD.WebApi.Host.Controllers.Catalog;

public class BanksController : VersionedApiController
{
    [HttpPost("search")]
    [OpenApiOperation("Danh sách ngân hàng.", "")]
    public Task<PaginationResponse<BankDto>> SearchAsync(SearchBanksRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Chi tiết ngân hàng.", "")]
    public Task<Result<BankDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBankRequest(id));
    }

    [HttpPost]
    [OpenApiOperation("Tạo mới ngân hàng.", "")]
    public Task<Result<Guid>> CreateAsync(CreateBankRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Cập nhật ngân hàng.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBankRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Xóa ngân hàng.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBankRequest(id));
    }

}