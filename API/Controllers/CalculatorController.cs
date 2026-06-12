using Microsoft.AspNetCore.Mvc;
using Taggy.Application.DTOs;
using Taggy.Application.Interfaces;

namespace Taggy.API.Controllers;

[ApiController]
[Route("")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculationService calculationService;

    public CalculatorController(ICalculationService _calculationService)
    {
        calculationService = _calculationService;
    }

    [HttpGet("emission")]
    public async Task<IActionResult> GetEmissionCalculation([FromQuery] EmissionCalculationDto dto)
    {
        try
        {
            var result = await calculationService.GetEmission(dto, CalculationCategory.EmissionAvoided);
            return Ok(result);
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

    [HttpGet("fuel-saving")]
    public async Task<IActionResult> GetFuelSavingCalculation([FromQuery] EmissionCalculationDto dto)
    {
        try
        {
            var result = await calculationService.GetEmission(dto, CalculationCategory.FuelSaving);
            return Ok(result);
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

    [HttpGet("paper-emission")]
    public async Task<IActionResult> GetPaperEmissionCalculation([FromQuery] EmissionCalculationDto dto)
    {
        try
        {
            var result = await calculationService.GetEmission(dto, CalculationCategory.PaperEmission);
            return Ok(result);
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

    [HttpGet("emission-series")]
    public async Task<IActionResult> GetEmissionChart([FromQuery] EmissionCalculationDto dto)
    {
        try
        {
            var result = await calculationService.GetChartData(dto, CalculationCategory.EmissionAvoided);
            return Ok(result);
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

    [HttpGet("fuel-saving-series")]
    public async Task<IActionResult> GetFuelSavingChart([FromQuery] EmissionCalculationDto dto)
    {
        try
        {
            var result = await calculationService.GetChartData(dto, CalculationCategory.FuelSaving);
            return Ok(result);
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

    [HttpGet("paper-emission-series")]
    public async Task<IActionResult> GetPaperEmissionChart([FromQuery] EmissionCalculationDto dto)
    {
        try
        {
            var result = await calculationService.GetChartData(dto, CalculationCategory.PaperEmission);
            return Ok(result);
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }
}
