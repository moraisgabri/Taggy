using Microsoft.EntityFrameworkCore;
using Taggy.Domain.Entities;
using Taggy.Domain.Interfaces;
using Taggy.Infrastructure.Data;

namespace Taggy.Infrastructure.Repositories;

public class FuelRepository : BaseRepository<Fuel>, IBaseRepository, IFuelRepository 
{ 
  public FuelRepository(AppDbContext _db): base(_db) {}

  public async Task<Fuel?> GetAsyncById(Guid id)
  {
    return await db.Fuels.FindAsync(id);
  }

  public async Task AddAsync(Fuel fuel)
  {
    await db.Fuels.AddAsync(fuel);
  }

  public async Task<List<Fuel>> GetAll()
  {
    return await db.Fuels.ToListAsync();
  }

}
