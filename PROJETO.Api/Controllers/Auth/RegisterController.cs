using Microsoft.AspNetCore.Mvc;

using PROJETO.DTO.Request.Auth;
using PROJETO.Domain.Interfaces.Repository.Auth;

namespace PROJETO.Api.Controllers.Auth;

[ApiController]
[Route("v1/Auth/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IRegisterRepository _registerRepository;

    public RegisterController(IRegisterRepository registerRepository)
    {
        _registerRepository = registerRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            return Ok(await _registerRepository.Register(request));
        }
        catch (Exception)
        {
            return StatusCode(500, $"SERVER ERROR");
        }
    }
}
