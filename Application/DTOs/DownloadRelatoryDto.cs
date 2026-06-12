using System.ComponentModel.DataAnnotations;

namespace Taggy.Application.DTOs;

public class DownloadRelatoryDto
{
    [Required]
    public int Frequency { get; set; }
    
    [Required]
    public decimal Paper { get; set; }

    [Required]
    public decimal Emission { get; set; }
    
    [Required]
    public decimal Fuel { get; set; }

}
