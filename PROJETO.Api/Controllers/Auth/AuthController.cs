using Microsoft.AspNetCore.Mvc;
using PROJETO.Domain.UseCase.Abstractions.Auth;
using PROJETO.Domain.UseCase.Implementations.Auth;

namespace PROJETO.Api.Controllers.Auth;

[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly ILoginUseCase _loginUseCase;

    private readonly IRegisterUseCase _registerUseCase;

    public AuthController(LoginUseCase loginUseCase, IRegisterUseCase registerUseCase)
    {
        _loginUseCase = loginUseCase;
        _registerUseCase = registerUseCase;
    }
}
