using PROJETO.Infra.Models;
using PROJETO.DTO.Validator;
using PROJETO.Infra.Database;
using PROJETO.DTO.Request.Auth;
using PROJETO.Infra.Interfaces.Services;
using PROJETO.Domain.Interfaces.Repository.Auth;

using Microsoft.EntityFrameworkCore;

using System.IdentityModel.Tokens.Jwt;

namespace PROJETO.Domain.Repository.Auth;

public class LoginRepository : ILoginRepository
{
    private readonly IJwtService _jwtService;

    private readonly MyDbContext _dbContext;

    public LoginRepository(MyDbContext dbContext, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _jwtService = jwtService;
    }

    public async Task<JwtSecurityToken> Login(LoginRequest request)
    {
        LoginRequestValidator loginRequestValidator = new();
        var validator = loginRequestValidator.Validate(request);

        if (validator.IsValid)
        {
            UserModel userModel = await _dbContext.Users.FirstAsync(
                p =>
                    request.Email == p.Email
                    && BCrypt.Net.BCrypt.HashPassword(request.Password) == p.Password
            );

            return _jwtService.GetJwtSecurityToken(userModel);
        }

        throw new ArgumentException();
    }
}
