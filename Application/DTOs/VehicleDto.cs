using System;

namespace Taggy.Application.DTOs;

public class VehicleDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string EngineType { get; set; }
    public decimal Consumption { get; set; }
    public decimal Co2Emission { get; set; }
    public DateTime CreatedAt { get; set; }
}
