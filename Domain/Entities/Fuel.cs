namespace Taggy.Domain.Entities;

public class Fuel
{
    public Guid Id { get; set; }
    required public string Name { get; set; }
    required public string Type { get; set; }
    public decimal EfficiencyFactor { get; set; }
    public decimal Co2Factor { get; set; }
    public DateTime CreatedAt { get; set; }

    required public ICollection<VehicleFuel> VehicleFuels { get; set; }
    required public ICollection<EmissionCalculation> EmissionCalculations { get; set; }
}