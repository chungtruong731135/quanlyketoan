using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Items;
public class DeleteItemRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteItemRequest(Guid id) => Id = id;
}

public class DeleteItemRequestHandler : IRequestHandler<DeleteItemRequest, Guid>
{
    private readonly IRepositoryWithEvents<Item> _itemRepo;
    private readonly IStringLocalizer _t;

    public DeleteItemRequestHandler(IRepositoryWithEvents<Item> itemRepo, IStringLocalizer<DeleteItemRequestHandler> localizer) =>
        (_itemRepo, _t) = (itemRepo, localizer);

    public async Task<Guid> Handle(DeleteItemRequest request, CancellationToken cancellationToken)
    {
        // Lấy thông tin của mặt hàng cần xóa
        var item = await _itemRepo.GetByIdAsync(request.Id, cancellationToken);

        // Nếu không tìm thấy mặt hàng, ném ra một ngoại lệ NotFoundException
        _ = item ?? throw new NotFoundException(_t["Item {0} not found."]);

        // Xóa mặt hàng
        await _itemRepo.DeleteAsync(item, cancellationToken);

        return request.Id;
    }
}
