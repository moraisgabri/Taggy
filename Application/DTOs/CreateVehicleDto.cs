namespace Taggy.Application.DTOs;

public class CreateVehicleDto
{
    public required string Name { get; set; }
    public required string EngineType { get; set; }
    public decimal Consumption { get; set; }
    public decimal Co2Emission { get; set; }
}
