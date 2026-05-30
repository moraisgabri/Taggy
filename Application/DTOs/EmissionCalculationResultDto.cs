namespace Taggy.Application.DTOs;

public class EmissionCalculationResultDto
{
    public TimeScaleEnum Timescale { get; set; }
    public int Frequency { get; set; }
    public int TimescaleValue { get; set; }
    public decimal ConstantValue { get; set; }
    public string ConstantUnit { get; set; } = null!;
    public decimal TotalValue { get; set; }
    public decimal NormalizedTotalValue => decimal.Round(TotalValue, 4, MidpointRounding.AwayFromZero);
    public string Unit { get; set; } = null!;
    public string Description { get; set; } = null!;
}
