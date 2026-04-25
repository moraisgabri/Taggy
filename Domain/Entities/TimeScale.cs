namespace Taggy.Domain.Entities;

public class TimeScale
{
    public Guid Id { get; set; }
    public string Label { get; set; }
    public int Multiplier { get; set; }
    public string Description { get; set; }

    public ICollection<EmissionCalculation> EmissionCalculations { get; set; }
    public ICollection<WasteCalculation> WasteCalculations { get; set; }
}