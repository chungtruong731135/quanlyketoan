using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Accounts;
public class AccountByNameSpec : Specification<Account>, ISingleResultSpecification<Account>
{
    public AccountByNameSpec(string name) =>
        Query.Where(a => a.Name == name);
}
