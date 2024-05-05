using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Domain.Catalog;
public class Item : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string? Code { get; set; }
    public string? Unit { get; set; }
    public decimal? Price { get; set; }

    public Item(string name, string? code, string? unit, decimal? price)
    {
        Name = name;
        Code = code;
        Unit = unit;
        Price = price;
    }

    public Item Update(string name, string? code, string? unit, decimal? price)
    {
        Name = name;
        Code = code;
        Unit = unit;
        Price = price;
        return this;
    }
}
