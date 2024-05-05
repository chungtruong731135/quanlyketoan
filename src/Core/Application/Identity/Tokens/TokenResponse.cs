namespace TD.WebApi.Application.Identity.Tokens;

public record TokenResponse(string? Token, string? RefreshToken, DateTime? RefreshTokenExpiryTime, bool? IsAuthSuccessful, bool? IsTfaEnabled);