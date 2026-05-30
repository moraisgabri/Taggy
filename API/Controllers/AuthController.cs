using Microsoft.AspNetCore.Mvc;
using Taggy.Application.DTOs;
using Taggy.Application.Interfaces;
using Taggy.Application.Services;

namespace Taggy.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly IPdfService pdfService;

    public AuthController(IAuthService _authService, IPdfService _pdfService)
    {
        authService = _authService;
        pdfService = _pdfService;
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

    [HttpPost("pdf")]
    public async Task<IActionResult> GeneratePdf()
    {
        try
        {
            byte[] pdfBytes = await pdfService.GeneratePdf();
            return File(pdfBytes, "application/pdf", "relatorio.pdf");
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

}