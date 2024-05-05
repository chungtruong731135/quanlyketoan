namespace TD.WebApi.Application.Identity.Tokens;

public record LoginLdapRequest(string UserName, string Password);

public class LoginLdapRequestValidator : CustomValidator<LoginLdapRequest>
{
    public LoginLdapRequestValidator(IStringLocalizer<LoginLdapRequestValidator> T)
    {
        RuleFor(p => p.UserName).Cascade(CascadeMode.Stop)
            .NotEmpty();

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}