using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Accounts;
public class AccountDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public Guid? BankId { get; set; }
    public string? BankName { get; set; }
    public string? Description { get; set; }
    public decimal? Balance { get; set; }
}
