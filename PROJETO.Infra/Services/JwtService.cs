using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using PROJETO.Infra.Identity;
using PROJETO.Infra.Interfaces.Services;
using PROJETO.Infra.Models;

namespace PROJETO.Domain.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public JwtSecurityToken GenerateToken(List<Claim> authClaim)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            expires: DateTime.Now.AddMinutes(30),
            claims: authClaim,
            signingCredentials: new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return token;
    }

    public void ValidateToken(string token, out SecurityToken securityToken)
    {
        new JwtSecurityTokenHandler().ValidateToken(
            token,
            new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
                )
            },
            out securityToken
        );
    }

    public List<Claim> CreateClaims(int id, string name, string email, string role)
    {
        return new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, id.ToString()),
            new(JwtRegisteredClaimNames.Name, name),
            new(JwtRegisteredClaimNames.Email, email),
            new(PolicyRules.ClaimTitle, role)
        };
    }

    public JwtSecurityToken GetJwtSecurityToken(UserModel model)
    {
        List<Claim> claims = CreateClaims(model.Id, model.Name, model.Email, model.Role);
        return GenerateToken(claims);
    }
}
