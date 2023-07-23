using System.IdentityModel.Tokens.Jwt;

using PROJETO.DTO.Request.Auth;

namespace PROJETO.Domain.Interfaces.Repository;

public interface IAuthRepository
{
    Task RegisterAsync(RegisterRequest request);

    Task<JwtSecurityToken> LoginAsync(LoginRequest request);
}
