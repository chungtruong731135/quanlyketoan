namespace TD.WebApi.Infrastructure.Ldap;

public class LDAPSettings
{
    public bool Enable { get; set; }
    public int Port { get; set; } = 389;
    public string Zone { get; set; } = string.Empty;
    public string Domain { get; set; } = string.Empty;
    public string Subdomain { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string LDAPserver { get; set; } = string.Empty;
    public string LDAPQueryBase { get; set; } = string.Empty;

    public string Crew { get; set; } = string.Empty;
    public string Managers { get; set; } = string.Empty;
}