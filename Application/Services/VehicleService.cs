using Taggy.Application.DTOs;
using Taggy.Application.Interfaces;
using Taggy.Domain.Entities;
using Taggy.Domain.Interfaces;

namespace Taggy.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository vehicleRepository;

    public VehicleService(IVehicleRepository _vehicleRepository)
    {
        vehicleRepository = _vehicleRepository;
    }

    public async Task<List<VehicleDto>> GetAllVehicles()
    {
        var vehicles = await vehicleRepository.GetAll();
        return vehicles.Select(v => new VehicleDto
        {
            Id = v.Id.ToString(),
            Name = v.Name,
            EngineType = v.EngineType,
            Consumption = v.Consumption,
            Co2Emission = v.Co2Emission,
            CreatedAt = v.CreatedAt
        }).ToList();
    }

    public async Task<VehicleDto> GetVehicleById(string id)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(Guid.Parse(id)) ?? throw new Exception("Vehicle not found");
        return new VehicleDto
        {
            Id = vehicle.Id.ToString(),
            Name = vehicle.Name,
            EngineType = vehicle.EngineType,
            Consumption = vehicle.Consumption,
            Co2Emission = vehicle.Co2Emission,
            CreatedAt = vehicle.CreatedAt
        };
    }

    public async Task<VehicleDto> CreateVehicle(CreateVehicleDto createVehicleDto)
    {
        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            Name = createVehicleDto.Name,
            EngineType = createVehicleDto.EngineType,
            Consumption = createVehicleDto.Consumption,
            Co2Emission = createVehicleDto.Co2Emission,
            CreatedAt = DateTime.UtcNow,
            UserVehicles = new List<UserVehicle>(),
            VehicleFuels = new List<VehicleFuel>(),
            EmissionCalculations = new List<EmissionCalculation>()
        };

        await vehicleRepository.AddAsync(vehicle);
        await vehicleRepository.SaveChangesAsync();

        return new VehicleDto
        {
            Id = vehicle.Id.ToString(),
            Name = vehicle.Name,
            EngineType = vehicle.EngineType,
            Consumption = vehicle.Consumption,
            Co2Emission = vehicle.Co2Emission,
            CreatedAt = vehicle.CreatedAt
        };
    }

    public async Task<VehicleDto> EditVehicle(string id, EditVehicleDto editVehicleDto)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(Guid.Parse(id)) ?? throw new Exception("Vehicle not found");

        vehicle.Name = editVehicleDto.Name;
        vehicle.EngineType = editVehicleDto.EngineType;
        vehicle.Consumption = editVehicleDto.Consumption;
        vehicle.Co2Emission = editVehicleDto.Co2Emission;

        var updatedVehicle = await vehicleRepository.Edit(vehicle) ?? throw new Exception("Vehicle not found");

        return new VehicleDto
        {
            Id = updatedVehicle.Id.ToString(),
            Name = updatedVehicle.Name,
            EngineType = updatedVehicle.EngineType,
            Consumption = updatedVehicle.Consumption,
            Co2Emission = updatedVehicle.Co2Emission,
            CreatedAt = updatedVehicle.CreatedAt
        };
    }

    public async Task DeleteVehicle(string id)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(Guid.Parse(id)) ?? throw new Exception("Vehicle not found");
        await vehicleRepository.Delete(vehicle);
    }
}
