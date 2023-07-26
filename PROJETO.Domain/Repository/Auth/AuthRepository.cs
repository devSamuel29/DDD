using PROJETO.Infra.Models;
using PROJETO.DTO.Validator;
using PROJETO.Infra.Database;
using PROJETO.DTO.Request.Auth;
using PROJETO.Domain.Interfaces;
using PROJETO.Domain.Interfaces.Services;
using PROJETO.Domain.Interfaces.Repository;

using System.IdentityModel.Tokens.Jwt;

using Microsoft.EntityFrameworkCore;

using AutoMapper;

namespace PROJETO.Domain.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly MyDbContext _dbContext;

    private readonly IJwtService _jwtService;

    private readonly IMapper _mapper;

    private readonly IEmailService _emailService;

    public AuthRepository(
        MyDbContext dbContext,
        IJwtService jwtService,
        IEmailService emailService,
        IMapper mapper
    )
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _jwtService = jwtService;
        _emailService = emailService;
    }

    public async Task<JwtSecurityToken> LoginAsync(LoginRequest request)
    {
        var validator = new LoginRequestValidator();
        var validation = validator.Validate(request);
        if (validation.IsValid)
        {
            var dbUser = await _dbContext.Users.FirstAsync(p => p.Email == request.Email);
            var isValidHash = BCrypt.Net.BCrypt.Verify(request.Password, dbUser.Password);

            if (isValidHash)
            {
                var claims = await _jwtService.CreateClaimsAsync(
                    dbUser.Id,
                    dbUser.Name,
                    dbUser.Email,
                    dbUser.Role
                );
                // await _emailService.SendEmail(
                //     dbUser.Email,
                //     "LOGIN - PROJETO",
                //     "VOCÊ ACABA DE FAZER LOGIN!"
                // );
                return await _jwtService.GetTokenAsync(claims);
            }
            throw new InvalidDataException("Dados incorretos!");
        }
        throw new FormatException(validation.ToString());
    }

    public async Task<JwtSecurityToken> RegisterAsync(RegisterRequest request)
    {
        var validator = new RegisterRequestValidator();
        var validation = validator.Validate(request);

        if (validation.IsValid)
        {
            await _dbContext.Users.AddAsync(_mapper.Map<UserModel>(request));
            await _dbContext.SaveChangesAsync();
            // await _emailService.SendEmail(
            //     request.Email,
            //     "CADASTRO - PROJETO",
            //     "VOCÊ ACABA DE FAZER SE CADASTRAR!"
            // );
            return await LoginAsync(_mapper.Map<LoginRequest>(request));
        }
        throw new InvalidDataException(validation.ToString());
    }
}
