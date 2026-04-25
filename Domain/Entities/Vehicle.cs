namespace Taggy.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string EngineType { get; set; }
    public decimal Consumption { get; set; }
    public decimal Co2Emission { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<UserVehicle> UserVehicles { get; set; }
    public ICollection<VehicleFuel> VehicleFuels { get; set; }
    public ICollection<EmissionCalculation> EmissionCalculations { get; set; }
}