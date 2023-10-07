using PROJETO.DTO.Request.Auth;

using System.IdentityModel.Tokens.Jwt;

namespace PROJETO.Domain.Interfaces.Repository.Auth;

public interface IRegisterRepository
{
    Task<JwtSecurityToken> Register(RegisterRequest request);
}