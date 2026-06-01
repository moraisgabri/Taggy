using Microsoft.EntityFrameworkCore;
using Taggy.Domain.Entities;
using Taggy.Domain.Interfaces;
using Taggy.Infrastructure.Data;

namespace Taggy.Infrastructure.Repositories;

public class VehicleRepository(AppDbContext context) : IVehicleRepository
{
    public async Task<List<Vehicle>> GetAll() =>
        await context.Vehicles.ToListAsync();

    public async Task<Vehicle?> GetByIdAsync(Guid id) =>
        await context.Vehicles.FindAsync(id);

    public async Task AddAsync(Vehicle vehicle) =>
        await context.Vehicles.AddAsync(vehicle);

    public async Task<Vehicle?> Edit(Vehicle updatedVehicle)
    {
        context.Vehicles.Update(updatedVehicle);
        await context.SaveChangesAsync();
        return await context.Vehicles.FindAsync(updatedVehicle.Id);
    }

    public async Task Delete(Vehicle vehicle)
    {
        context.Vehicles.Remove(vehicle);
        await context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}
