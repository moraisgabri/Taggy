using Microsoft.AspNetCore.Mvc;
using Taggy.Application.DTOs;
using Taggy.Application.Interfaces;

namespace Taggy.API.Controllers;

[ApiController]
[Route("vehicles")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService vehicleService;

    public VehicleController(IVehicleService _vehicleService)
    {
        vehicleService = _vehicleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetVehicles()
    {
        try
        {
            return Ok(await vehicleService.GetAllVehicles());
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicleById(string id)
    {
        try
        {
            return Ok(await vehicleService.GetVehicleById(id));
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateVehicle(CreateVehicleDto createVehicleDto)
    {
        try
        {
            var vehicle = await vehicleService.CreateVehicle(createVehicleDto);
            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.Id }, vehicle);
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditVehicle(string id, EditVehicleDto editVehicleDto)
    {
        try
        {
            return Ok(await vehicleService.EditVehicle(id, editVehicleDto));
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(string id)
    {
        try
        {
            await vehicleService.DeleteVehicle(id);
            return NoContent();
        }
        catch (Exception err)
        {
            return BadRequest(new { message = err.Message });
        }
    }
}
