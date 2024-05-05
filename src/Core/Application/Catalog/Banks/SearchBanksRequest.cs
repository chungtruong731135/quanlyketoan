namespace TD.WebApi.Application.Catalog.Banks;

public class SearchBanksRequest : PaginationFilter, IRequest<PaginationResponse<BankDto>>
{
    public bool? IsActive { get; set; }
}

public class BanksBySearchRequestSpec : EntitiesByPaginationFilterSpec<Bank, BankDto>
{
    public BanksBySearchRequestSpec(SearchBanksRequest request)
        : base(request) =>
        Query
        .Where(p => p.IsActive == request.IsActive, request.IsActive.HasValue)
        ;
}

public class SearchBanksRequestHandler : IRequestHandler<SearchBanksRequest, PaginationResponse<BankDto>>
{
    private readonly IReadRepository<Bank> _repository;

    public SearchBanksRequestHandler(IReadRepository<Bank> repository) => _repository = repository;

    public async Task<PaginationResponse<BankDto>> Handle(SearchBanksRequest request, CancellationToken cancellationToken)
    {
        var spec = new BanksBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}