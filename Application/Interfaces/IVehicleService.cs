using Taggy.Application.DTOs;

namespace Taggy.Application.Interfaces;

public interface IVehicleService
{
    Task<List<VehicleDto>> GetAllVehicles();
    Task<VehicleDto> GetVehicleById(string id);
    Task<VehicleDto> CreateVehicle(CreateVehicleDto createVehicleDto);
    Task<VehicleDto> EditVehicle(string id, EditVehicleDto editVehicleDto);
    Task DeleteVehicle(string id);
}
