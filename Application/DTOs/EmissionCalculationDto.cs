using System.ComponentModel.DataAnnotations;

namespace Taggy.Application.DTOs;

public class EmissionCalculationDto
{
    [Required]
    public TimeScaleEnum Timescale { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Frequency must be at least 1")]
    public int Frequency { get; set; }
}
