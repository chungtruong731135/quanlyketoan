using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Items;
public class SearchItemsRequest : PaginationFilter, IRequest<PaginationResponse<ItemDto>>
{
    public string? Name { get; set; }
}

public class ItemsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Item, ItemDto>
{
    public ItemsBySearchRequestSpec(SearchItemsRequest request)
        : base(request)
    {
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            Query.Where(i => i.Name.Contains(request.Name));
        }

        Query.OrderBy(c => c.Name, !request.HasOrderBy());
    }
}

public class SearchItemsRequestHandler : IRequestHandler<SearchItemsRequest, PaginationResponse<ItemDto>>
{
    private readonly IReadRepository<Item> _repository;

    public SearchItemsRequestHandler(IReadRepository<Item> repository) => _repository = repository;

    public async Task<PaginationResponse<ItemDto>> Handle(SearchItemsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ItemsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
