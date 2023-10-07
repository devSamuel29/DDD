using PROJETO.Infra.Models;
using System.IdentityModel.Tokens.Jwt;

namespace PROJETO.Infra.Interfaces.Services;

public interface IJwtService
{
    JwtSecurityToken GetJwtSecurityToken(UserModel model);
}