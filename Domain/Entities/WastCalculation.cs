namespace Taggy.Domain.Entities;

public class WasteCalculation
{
    public Guid Id { get; set; }
    required public string TollName { get; set; }
    public int Lanes { get; set; }
    public int VehiclesPerDay { get; set; }
    public decimal NonPrintRate { get; set; }
    public decimal TicketWeight { get; set; }

    public Guid TimeScaleId { get; set; }
    public decimal TotalWasteKg { get; set; }
    public DateTime CreatedAt { get; set; }

    required public TimeScale TimeScale { get; set; }
}