namespace Taggy.Domain.Entities;

public class VehicleFuel
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public Guid FuelId { get; set; }
    public decimal AdjustedConsumption { get; set; }
    public decimal AdjustedEmission { get; set; }

    required public Vehicle Vehicle { get; set; }
    required public Fuel Fuel { get; set; }
}