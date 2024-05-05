namespace TD.WebApi.Application.Catalog.Banks;

public class GetBankRequest : IRequest<Result<BankDto>>
{
    public Guid Id { get; set; }

    public GetBankRequest(Guid id) => Id = id;
}

public class BankByIdSpec : Specification<Bank, BankDto>, ISingleResultSpecification
{
    public BankByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetBankRequestHandler : IRequestHandler<GetBankRequest, Result<BankDto>>
{
    private readonly IRepository<Bank> _repository;
    private readonly IStringLocalizer _t;

    public GetBankRequestHandler(IRepository<Bank> repository, IStringLocalizer<GetBankRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<Result<BankDto>> Handle(GetBankRequest request, CancellationToken cancellationToken)
    {
       var item = await _repository.FirstOrDefaultAsync(
           (ISpecification<Bank, BankDto>) new BankByIdSpec(request.Id), cancellationToken)
       ?? throw new NotFoundException(_t["Bank {0} Not Found.", request.Id]);
       return Result<BankDto>.Success(item);
    }
}