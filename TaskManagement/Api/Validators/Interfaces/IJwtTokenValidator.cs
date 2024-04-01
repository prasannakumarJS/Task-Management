using System.IdentityModel.Tokens.Jwt;

namespace Api.Validators
{
    public interface IJwtTokenValidator
    {
        bool ValidateToken(string token);
    }
}
