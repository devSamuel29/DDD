using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PROJETO.Domain.Identity;

namespace PROJETO.Domain.Interfaces.Services;

public interface IJwtService
{
    Task<string> FormatToken(string token);

    Task<JwtSecurityToken> GetTokenAsync(List<Claim> authClaim);

    Task<bool> ReadTokenAsync(string token);

    Task<Claims> GetClaimsAsync(string token);

    Task<List<Claim>> CreateClaimsAsync(int id, string name, string email, string role);
}
