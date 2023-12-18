using System.IdentityModel.Tokens.Jwt;

namespace PROJETO.Infra.DataSources.Abstractions.SqlServer.Auth;

public interface IAuthDataSource
{
    Task<JwtSecurityToken> SignUpAsync();

    Task<JwtSecurityToken> SignInAsync();
}
