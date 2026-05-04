namespace Taggy.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; set; }
    required public string Name { get; set; }
    required public string EngineType { get; set; }
    public decimal Consumption { get; set; }
    public decimal Co2Emission { get; set; }
    public DateTime CreatedAt { get; set; }

    required public ICollection<UserVehicle> UserVehicles { get; set; }
    required public ICollection<VehicleFuel> VehicleFuels { get; set; }
    required public ICollection<EmissionCalculation> EmissionCalculations { get; set; }
}