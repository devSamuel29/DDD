using Microsoft.AspNetCore.Mvc;

using PROJETO.DTO.Request.Auth;
using PROJETO.Domain.Interfaces.Repository.Auth;

namespace PROJETO.Api.Controllers.Auth;

[ApiController]
[Route("v1/Auth/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginRepository _loginRepository;

    public LoginController(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            return Ok(await _loginRepository.Login(request));
        }
        catch (Exception e)
        {
            return StatusCode(500, $"SERVER ERROR: {e.Message}");
        }
    }
}
