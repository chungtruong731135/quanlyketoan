using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Accounts;
public class UpdateAccountRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public Guid? BankId { get; set; }
    public string? Description { get; set; }
    public decimal? Balance { get; set; }
}

public class UpdateAccountRequestValidator : CustomValidator<UpdateAccountRequest>
{
    public UpdateAccountRequestValidator(IRepository<Account> repository, IStringLocalizer<UpdateAccountRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(256)
            .MustAsync(async (item, name, ct) =>
                    await repository.FirstOrDefaultAsync(new AccountByNameSpec(name), ct)
                        is not Account existingItem || existingItem.Id == item.Id)
                .WithMessage((_, name) => T["Account {0} already Exists.", name]);
}

public class UpdateAccountRequestHandler : IRequestHandler<UpdateAccountRequest, Result<Guid>>
{
    private readonly IRepositoryWithEvents<Account> _repository;
    private readonly IStringLocalizer _t;

    public UpdateAccountRequestHandler(IRepositoryWithEvents<Account> repository, IStringLocalizer<UpdateAccountRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateAccountRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(_t["Account {0} Not Found.", request.Id]);

        item.Update(request.Name, request.Code, request.BankId, request.Description, request.Balance);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}
