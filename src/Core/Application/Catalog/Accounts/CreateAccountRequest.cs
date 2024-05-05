using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Accounts;
public class CreateAccountRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; } = default!;
    public string? Code { get; set; }
    public Guid? BankId { get; set; }
    public string? Description { get; set; }
    public decimal? Balance { get; set; }
}

public class CreateAccountRequestValidator : CustomValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator(IReadRepository<Account> repository, IStringLocalizer<CreateAccountRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(256)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new AccountByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Account {0} already Exists.", name]);
}

public class CreateAccountRequestHandler : IRequestHandler<CreateAccountRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Account> _repository;

    public CreateAccountRequestHandler(IRepositoryWithEvents<Account> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var account = new Account(request.Name, request.Code, request.BankId, request.Description, request.Balance);

        await _repository.AddAsync(account, cancellationToken);

        return Result<Guid>.Success(account.Id);
    }
}
