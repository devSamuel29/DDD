using PROJETO.Domain.Interfaces.Services;

using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using PROJETO.Domain.Identity;

namespace PROJETO.Domain.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> FormatToken(string token)
    {
        if (token.StartsWith("Bearer"))
        {
            string[] splitedToken = token.Split(" ");
            if (await ReadTokenAsync(splitedToken[1]))
            {
                token = splitedToken[1];
            }
            else
            {
                throw new Exception("");
            }
        }

        return await Task.FromResult(token);
    }

    public async Task<JwtSecurityToken> GetTokenAsync(List<Claim> authClaim)
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

        return await Task.FromResult(token);
    }

    public async Task<bool> ReadTokenAsync(string token)
    {
        token = await FormatToken(token);
        var tokenHandler = await new JwtSecurityTokenHandler().ValidateTokenAsync(
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
            }
        );

        return await Task.FromResult(tokenHandler.IsValid);
    }

    public async Task<Claims> GetClaimsAsync(string token)
    {
        bool isValidToken = await ReadTokenAsync(token);

        if (isValidToken)
        {
            var handler = new JwtSecurityTokenHandler();
            IList<string> claimsList = new List<string>();

            foreach (var claim in handler.ReadJwtToken(token).Claims)
            {
                claimsList.Add(claim.Value);
            }

            Claims claims = new Claims()
            {
                Id = int.Parse(claimsList[0]),
                Name = claimsList[1],
                Email = claimsList[2],
                Role = claimsList[3],
                Exp = int.Parse(claimsList[4]),
            };

            return await Task.FromResult(claims);
        }

        throw new InvalidDataException("invalid token");
    }

    public async Task<List<Claim>> CreateClaimsAsync(
        int id,
        string name,
        string email,
        string role
    )
    {
        return await Task.FromResult(
            new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, id.ToString()),
                new(JwtRegisteredClaimNames.Name, name),
                new(JwtRegisteredClaimNames.Email, email),
                new(PolicyRules.ClaimTitle, role)
            }
        );
    }
}
