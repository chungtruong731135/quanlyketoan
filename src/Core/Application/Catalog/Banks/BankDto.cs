namespace TD.WebApi.Application.Catalog.Banks;

public class BankDto : IDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public string? Bin { get; set; }
    public string? Code { get; set; }
    public string? Key { get; set; }
    public string? SwiftCode { get; set; }
    public bool? IsActive { get; set; }
}