using Taggy.Domain.Entities;

namespace Taggy.Domain.Interfaces;

public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAll();
    Task<Vehicle?> GetByIdAsync(Guid id);
    Task AddAsync(Vehicle vehicle);
    Task<Vehicle?> Edit(Vehicle updatedVehicle);
    Task Delete(Vehicle vehicle);
    Task SaveChangesAsync();
}
