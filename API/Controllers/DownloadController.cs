using Microsoft.AspNetCore.Mvc;
using Taggy.Application.DTOs;
using Taggy.Application.Interfaces;
using Taggy.Application.Services;

namespace Taggy.API.Controllers;

[ApiController]
[Route("download")]
public class DownloadController : ControllerBase
{
    private readonly IPdfService pdfService;

    public DownloadController(IPdfService _pdfService)
    {
        pdfService = _pdfService;
    }

    [HttpPost("pdf")]
    public async Task<IActionResult> GeneratePdf(DownloadRelatoryDto dto)
    {
        try
        {
            byte[] pdfBytes = await pdfService.GeneratePdf(dto.Id);
            return File(pdfBytes, "application/pdf", "relatorio.pdf");
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

}