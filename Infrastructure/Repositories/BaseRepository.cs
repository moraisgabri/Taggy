using Taggy.Infrastructure.Data;
using Taggy.Domain.Interfaces;

public class BaseRepository<T>(AppDbContext _db): IBaseRepository
{
  public AppDbContext db = _db;

  public async Task SaveChangesAsync()
  {
    await db.SaveChangesAsync();
  }

  // public async Task<T> findById(T id)
  // {
  //   return await db.FindAsync(id);
  // }
}
