using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Items;
public class ItemByNameSpec : Specification<Item>, ISingleResultSpecification
{
    public ItemByNameSpec(string name) =>
        Query.Where(i => i.Name == name);
}
