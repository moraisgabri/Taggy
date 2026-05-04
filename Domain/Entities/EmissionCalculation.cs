namespace Taggy.Domain.Entities;

public class EmissionCalculation
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public Guid FuelId { get; set; }
    public Guid TimeScaleId { get; set; }

    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public decimal TotalEmission { get; set; }
    public DateTime CreatedAt { get; set; }

    required public Vehicle Vehicle { get; set; }
    required public Fuel Fuel { get; set; }
    required public TimeScale TimeScale { get; set; }
}