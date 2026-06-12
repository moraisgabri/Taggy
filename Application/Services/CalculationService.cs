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

        int timescaleMultiplier = dto.Timescale switch
        {
            TimeScaleEnum.D => 1,
            TimeScaleEnum.M => 30,
            TimeScaleEnum.Y => 365,
            _ => throw new ArgumentOutOfRangeException(nameof(dto.Timescale), dto.Timescale, "Timescale inválido.")
        };

        int timescaleQuantity = dto.TimescaleValue * dto.Frequency * timescaleMultiplier;
        decimal totalValue = constant * timescaleQuantity;

        var result = new EmissionCalculationResultDto
        {
            Timescale = dto.Timescale,
            Frequency = dto.Frequency,
            TimescaleValue = dto.TimescaleValue,
            ConstantValue = constant,
            ConstantUnit = constantUnit,
            TotalValue = totalValue,
            Unit = unit,
            Description = description
        };

        return Task.FromResult(result);
    }

    public Task<IEnumerable<ChartPointDto>> GetChartData(EmissionCalculationDto dto, CalculationCategory category)
    {
        decimal constant;
        string labelPrefix;

        switch (category)
        {
            case CalculationCategory.EmissionAvoided:
                constant = EmissionAvoidedKgPerFrequency;
                break;
            case CalculationCategory.FuelSaving:
                constant = FuelSavedMlPerFrequency;
                break;
            case CalculationCategory.PaperEmission:
                constant = PaperEmissionGPerFrequency;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(category), category, "Categoria de cálculo inválida.");
        }

        int timescaleMultiplier = dto.Timescale switch
        {
            TimeScaleEnum.D => 1,
            TimeScaleEnum.M => 30,
            TimeScaleEnum.Y => 365,
            _ => throw new ArgumentOutOfRangeException(nameof(dto.Timescale), dto.Timescale, "Timescale inválido.")
        };

        decimal targetTotal = constant * dto.Frequency * dto.TimescaleValue * timescaleMultiplier;

        labelPrefix = dto.Timescale switch
        {
            TimeScaleEnum.D => "Dia",
            TimeScaleEnum.M => "Mês",
            TimeScaleEnum.Y => "Ano",
            _ => ""
        };

        int flatChance = dto.Timescale switch
        {
            TimeScaleEnum.D => 50,
            TimeScaleEnum.M => 30,
            TimeScaleEnum.Y => 15,
            _ => 20
        };

        int maxFlatLength = dto.Timescale switch
        {
            TimeScaleEnum.D => 3,
            TimeScaleEnum.M => 2,
            TimeScaleEnum.Y => 1,
            _ => 1
        };

        int points = Math.Max(dto.TimescaleValue, 1);
        var random = new Random(dto.TimescaleValue + dto.Frequency * 10 + (int)category * 100 + (int)dto.Timescale);
        var weights = new decimal[points];

        int flatRemaining = 0;
        for (int i = 0; i < points; i++)
        {
            if (flatRemaining > 0)
            {
                weights[i] = 0.2m + (decimal)random.NextDouble() * 0.15m;
                flatRemaining--;
                continue;
            }

            if (i > 0 && random.Next(100) < flatChance)
            {
                flatRemaining = random.Next(1, maxFlatLength + 1) - 1;
                weights[i] = 0.2m + (decimal)random.NextDouble() * 0.15m;
                continue;
            }

            weights[i] = 0.85m + (decimal)random.NextDouble() * 0.3m;
            if (i > 0)
            {
                weights[i] = (weights[i] + weights[i - 1]) / 2;
            }
        }

        decimal weightSum = weights.Sum();
        if (weightSum <= 0)
        {
            for (int i = 0; i < points; i++)
            {
                weights[i] = 1m;
            }
            weightSum = points;
        }

        var list = new List<ChartPointDto>(points);
        decimal cumulative = 0;

        for (int i = 0; i < points; i++)
        {
            decimal increment = Math.Round(targetTotal * weights[i] / weightSum, 4);
            cumulative += increment;

            if (i == points - 1)
            {
                cumulative = targetTotal;
            }

            list.Add(new ChartPointDto
            {
                Label = $"{labelPrefix} {i + 1}",
                Value = cumulative
            });
        }

        return Task.FromResult<IEnumerable<ChartPointDto>>(list);
    }
}
