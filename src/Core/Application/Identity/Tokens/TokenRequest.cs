namespace TD.WebApi.Application.Identity.Tokens;

public record TokenRequest(string? UserName, string? Email, string Password);

public class TokenRequestValidator : CustomValidator<TokenRequest>
{
    public TokenRequestValidator(IStringLocalizer<TokenRequestValidator> T)
    {

        RuleFor(p => p).Must(x => !string.IsNullOrEmpty(x.UserName) || !string.IsNullOrEmpty(x.Email))
               .WithMessage(T["Username or Email is required."]);

        RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
            .NotEmpty().When(x => !string.IsNullOrEmpty(x.Email))
            .EmailAddress()
                .WithMessage(T["Invalid Email Address."]);

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}