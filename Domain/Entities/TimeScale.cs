namespace Taggy.Domain.Entities;

public class TimeScale
{
    public Guid Id { get; set; }
    required public string Label { get; set; }
    public int Multiplier { get; set; }
    required public string Description { get; set; }

    required public ICollection<EmissionCalculation> EmissionCalculations { get; set; }
    required public ICollection<WasteCalculation> WasteCalculations { get; set; }
}