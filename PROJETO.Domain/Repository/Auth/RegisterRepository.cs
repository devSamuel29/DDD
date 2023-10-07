using PROJETO.DTO.Validator;
using PROJETO.Infra.Database;
using PROJETO.DTO.Mapper.Auth;
using PROJETO.DTO.Request.Auth;
using PROJETO.Domain.Interfaces.Repository.Auth;

using System.IdentityModel.Tokens.Jwt;

namespace PROJETO.Domain.Repository.Auth;

public class RegisterRepository : IRegisterRepository
{
    private readonly RegisterMapper _mapper;

    private readonly ILoginRepository _loginRepository;

    private readonly MyDbContext _dbContext;

    public RegisterRepository(
        RegisterMapper mapper,
        ILoginRepository loginRepository,
        MyDbContext dbContext
    )
    {
        _mapper = mapper;
        _loginRepository = loginRepository;
        _dbContext = dbContext;
    }

    public async Task<JwtSecurityToken> Register(RegisterRequest request)
    {
        RegisterRequestValidator registerRequestValidator = new();
        var validator = registerRequestValidator.Validate(request); 
        
        if (validator.IsValid)
        {
            await _dbContext.Users.AddAsync(_mapper.RegisterRequestToModel(request));
            await _dbContext.SaveChangesAsync();
            return await _loginRepository.Login(_mapper.RegisterRequestToLogin(request));
        }

        throw new ArgumentException();
    }
}
