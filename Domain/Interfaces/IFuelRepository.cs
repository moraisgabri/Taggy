using Taggy.Domain.Entities;

namespace Taggy.Domain.Interfaces;

public interface IFuelRepository : IBaseRepository
{
    Task AddAsync(Fuel user);
    Task<List<Fuel>> GetAll();
}
