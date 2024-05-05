using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Items;
public class CreateItemRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Code { get; set; }
    public string? Unit { get; set; }
    public decimal? Price { get; set; }
}

public class CreateItemRequestValidator : CustomValidator<CreateItemRequest>
{
    public CreateItemRequestValidator(IReadRepository<Item> repository, IStringLocalizer<CreateItemRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(100)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new ItemByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Item {0} already exists.", name]);

}

public class CreateItemRequestHandler : IRequestHandler<CreateItemRequest, Guid>
{
    private readonly IRepositoryWithEvents<Item> _repository;

    public CreateItemRequestHandler(IRepositoryWithEvents<Item> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateItemRequest request, CancellationToken cancellationToken)
    {
        var item = new Item(request.Name, request.Code, request.Unit, request.Price);

        await _repository.AddAsync(item, cancellationToken);

        return item.Id;
    }
}
