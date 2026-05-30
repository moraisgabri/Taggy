using Taggy.Application.DTOs;
using Taggy.Application.Interfaces;

namespace Taggy.Application.Services;

public class CalculationService : ICalculationService
{
    public const decimal EmissionAvoidedKgPerFrequency = 0.3546m;
    public const decimal FuelSavedMlPerFrequency = 60.6m;
    public const decimal PaperEmissionGPerFrequency = 0.00185m;

    public Task<EmissionCalculationResultDto> GetEmission(EmissionCalculationDto dto, CalculationCategory category)
    {
        decimal constant;
        string constantUnit;
        string unit;
        string description;

        switch (category)
        {
            case CalculationCategory.EmissionAvoided:
                constant = EmissionAvoidedKgPerFrequency;
                constantUnit = "kg";
                unit = "kg";
                description = "Emissão evitada por frequência";
                break;
            case CalculationCategory.FuelSaving:
                constant = FuelSavedMlPerFrequency;
                constantUnit = "ml";
                unit = "ml";
                description = "Gasolina economizada por frequência";
                break;
            case CalculationCategory.PaperEmission:
                constant = PaperEmissionGPerFrequency;
                constantUnit = "g";
                unit = "g";
                description = "Emissão de papel por frequência";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(category), category, "Categoria de cálculo inválida.");
        }

        decimal totalValue = constant * dto.Frequency;

        var result = new EmissionCalculationResultDto
        {
            Timescale = dto.Timescale,
            Frequency = dto.Frequency,
            ConstantValue = constant,
            ConstantUnit = constantUnit,
            TotalValue = totalValue,
            Unit = unit,
            Description = description
        };

        return Task.FromResult(result);
    }
}
