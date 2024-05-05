namespace TD.WebApi.Application.Catalog.Banks;

public class UpdateBankRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public string? Bin { get; set; }
    public string? Code { get; set; }
    public string? Key { get; set; }
    public string? SwiftCode { get; set; }
    public bool? IsActive { get; set; } = true;
}

public class UpdateBankRequestValidator : CustomValidator<UpdateBankRequest>
{
    public UpdateBankRequestValidator(IRepository<Bank> repository, IStringLocalizer<UpdateBankRequestValidator> T) =>
        RuleFor(p => p.Code)
            .NotEmpty()
            .MaximumLength(256)
            .MustAsync(async (item, name, ct) =>
                    await repository.FirstOrDefaultAsync(new BankByCodeSpec(name), ct)
                        is not Bank existingItem || existingItem.Id == item.Id)
                .WithMessage((_, name) => T["Bank {0} already Exists.", name]);
}

public class UpdateBankRequestHandler : IRequestHandler<UpdateBankRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Bank> _repository;
    private readonly IStringLocalizer _t;

    public UpdateBankRequestHandler(IRepositoryWithEvents<Bank> repository, IStringLocalizer<UpdateBankRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Result<Guid>> Handle(UpdateBankRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item
        ?? throw new NotFoundException(_t["Bank {0} Not Found.", request.Id]);

        item.Update(request.Name, request.ShortName, request.Bin, request.Code, request.Key, request.SwiftCode, request.IsActive);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}