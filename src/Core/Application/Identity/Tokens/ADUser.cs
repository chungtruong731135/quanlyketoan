using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Identity.Tokens;
public class ADUser
{
    public Guid objectGUID { get; set; }

    public string sAMAccountName { get; set; } = string.Empty;
    public string displayName { get; set; } = string.Empty;
    public string mail { get; set; } = string.Empty;

    public DateTime whenCreated { get; set; }

    public List<string> memberOf { get; set; } = new List<string>();
}
