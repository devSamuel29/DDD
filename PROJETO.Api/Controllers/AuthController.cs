using PROJETO.Api.Routes;
using PROJETO.Domain.Interfaces.Repository;

using Microsoft.AspNetCore.Mvc;
using PROJETO.DTO.Request.Auth;

namespace PROJETO.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [HttpPost(AuthRoutes.LOGIN_ROUTE)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            return Ok(await _authRepository.LoginAsync(request));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost(AuthRoutes.REGISTER_ROUTE)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            await _authRepository.RegisterAsync(request);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
