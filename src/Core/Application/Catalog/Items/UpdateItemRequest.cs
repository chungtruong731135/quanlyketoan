using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Items;
public class UpdateItemRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Code { get; set; }
    public string? Unit { get; set; }
    public decimal? Price { get; set; }
}

public class UpdateItemRequestValidator : CustomValidator<UpdateItemRequest>
{
    public UpdateItemRequestValidator(IRepository<Item> repository, IStringLocalizer<UpdateItemRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync(async (item, name, ct) =>
                    await repository.GetBySpecAsync(new ItemByNameSpec(name), ct)
                        is not Item existingItem || existingItem.Id == item.Id)
                .WithMessage((_, name) => T["Item {0} already exists.", name]);

    // Validate Unit and Price if necessary
}

public class UpdateItemRequestHandler : IRequestHandler<UpdateItemRequest, Guid>
{
    private readonly IRepositoryWithEvents<Item> _repository;
    private readonly IStringLocalizer _t;

    public UpdateItemRequestHandler(IRepositoryWithEvents<Item> repository, IStringLocalizer<UpdateItemRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateItemRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item
        ?? throw new NotFoundException(_t["Item {0} Not Found.", request.Id]);

        item.Update(request.Name, request.Code, request.Unit, request.Price);

        await _repository.UpdateAsync(item, cancellationToken);

        return request.Id;
    }
}
