namespace Taggy.Domain.Entities;

public class Fuel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal EfficiencyFactor { get; set; }
    public decimal Co2Factor { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<VehicleFuel> VehicleFuels { get; set; }
    public ICollection<EmissionCalculation> EmissionCalculations { get; set; }
}