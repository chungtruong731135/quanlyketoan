using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Domain.Catalog;
public class Account : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; } = default!;
    public string? Code { get; set; }
    public Guid? BankId { get; set; }
    public string? Description { get; set; }
    public decimal? Balance { get; set; }

    public Account(string name, string? code, DefaultIdType? bankId, string? description, decimal? balance)
    {
        Name = name;
        Code = code;
        BankId = bankId;
        Description = description;
        Balance = balance;
    }

    public Account Update(string name, string? code, DefaultIdType? bankId, string? description, decimal? balance)
    {
        Name = name;
        Code = code;
        BankId = bankId;
        Description = description;
        Balance = balance;
        return this;
    }
}
