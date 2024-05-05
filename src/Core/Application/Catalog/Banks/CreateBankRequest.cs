namespace TD.WebApi.Application.Catalog.Banks;

public class CreateBankRequest : IRequest<Result<Guid>>
{
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public string? Bin { get; set; }
    public string? Code { get; set; }
    public string? Key { get; set; }
    public string? SwiftCode { get; set; }
    public bool? IsActive { get; set; } = true;
}

public class CreateBankRequestValidator : CustomValidator<CreateBankRequest>
{
    public CreateBankRequestValidator(IReadRepository<Bank> repository, IStringLocalizer<CreateBankRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(256)
            .MustAsync(async (name, ct) => await repository.FirstOrDefaultAsync(new BankByCodeSpec(name), ct) is null)
                .WithMessage((_, name) => T["Bank {0} already Exists.", name]);
}

public class CreateBankRequestHandler : IRequestHandler<CreateBankRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Bank> _repository;

    public CreateBankRequestHandler(IRepositoryWithEvents<Bank> repository) => _repository = repository;

    public async Task<Result<Guid>> Handle(CreateBankRequest request, CancellationToken cancellationToken)
    {
        var item = new Bank(request.Name, request.ShortName, request.Bin, request.Code, request.Key, request.SwiftCode, request.IsActive);

        await _repository.AddAsync(item, cancellationToken);

        return Result<Guid>.Success(item.Id);
    }
}