namespace TD.WebApi.Application.Catalog.Banks;

public class BankByCodeSpec : Specification<Bank>, ISingleResultSpecification<Bank>
{
    public BankByCodeSpec(string code) =>
        Query.Where(b => b.Code == code);
}