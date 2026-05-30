using Taggy.Application.DTOs;

namespace Taggy.Application.Interfaces;

public interface ICalculationService
{
    Task<EmissionCalculationResultDto> GetEmission(EmissionCalculationDto dto, CalculationCategory category);
}
