using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Items;
public class GetItemRequest : IRequest<ItemDto>
{
    public Guid Id { get; set; }

    public GetItemRequest(Guid id) => Id = id;
}

public class ItemByIdSpec : Specification<Item, ItemDto>, ISingleResultSpecification
{
    public ItemByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetItemRequestHandler : IRequestHandler<GetItemRequest, ItemDto>
{
    private readonly IRepository<Item> _repository;
    private readonly IStringLocalizer _t;

    public GetItemRequestHandler(IRepository<Item> repository, IStringLocalizer<GetItemRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<ItemDto> Handle(GetItemRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Item, ItemDto>)new ItemByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Item {0} Not Found.", request.Id]);
}
