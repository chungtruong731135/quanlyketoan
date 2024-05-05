using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Domain.Catalog;
public class Bank : AuditableEntity, IAggregateRoot
{
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public string? Bin { get; set; }
    public string? Code { get; set; }
    public string? Key { get; set; }
    public string? SwiftCode { get; set; }
    public bool? IsActive { get; set; } = true;

    public Bank(string? name, string? shortName, string? bin, string? code, string? key, string? swiftCode, bool? isActive)
    {
        Name = name;
        ShortName = shortName;
        Bin = bin;
        Code = code;
        Key = key;
        SwiftCode = swiftCode;
        IsActive = isActive;
    }

    public Bank Update(string? name, string? shortName, string? bin, string? code, string? key, string? swiftCode, bool? isActive)
    {
        Name = name;
        ShortName = shortName;
        Bin = bin;
        Code = code;
        Key = key;
        SwiftCode = swiftCode;
        IsActive = isActive;
        return this;
    }
}