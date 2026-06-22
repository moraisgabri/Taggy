using Microsoft.AspNetCore.Mvc;
using Taggy.Application.DTOs;

namespace Taggy.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService _authService)
    {
        authService = _authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        try {
            return Ok(await authService.Register(dto));
        }
        catch (InvalidOperationException err)
        {
            return Conflict(new { message =  err.Message });
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        try
        {
            return Ok(await authService.Login(dto));
        }
        catch (Exception err)
        {
            return Unauthorized(new { message = err.Message });
        }
    }

    [HttpPost("getMe")]
    public async Task<IActionResult> GetMe(GetMeDto dto)
    {
        try
        {
            return Ok(await authService.GetMe(dto));
        }
        catch (Exception err)
        {
            return Unauthorized(new { message = err.Message });
        }
    }
}