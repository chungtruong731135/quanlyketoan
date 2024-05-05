using MediatR;
using Microsoft.AspNetCore.Mvc;
using TD.WebApi.Application.Catalog.Items;

namespace TD.WebApi.Host.Controllers.Catalog;
public class ItemsController : VersionedApiController
{
    private readonly IMediator _mediator;

    public ItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("search")]
    [OpenApiOperation("Search items using available filters.", "")]
    public Task<PaginationResponse<ItemDto>> SearchAsync(SearchItemsRequest request)
    {
        return _mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get item details.", "")]
    public Task<ItemDto> GetAsync(Guid id)
    {
        return _mediator.Send(new GetItemRequest(id));
    }

    [HttpPost]
    [OpenApiOperation("Create a new item.", "")]
    public Task<Guid> CreateAsync(CreateItemRequest request)
    {
        return _mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update an item.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateItemRequest request, Guid id)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        return Ok(await _mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete an item.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return _mediator.Send(new DeleteItemRequest(id));
    }
}
