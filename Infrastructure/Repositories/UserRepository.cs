using Microsoft.EntityFrameworkCore;
using Taggy.Domain.Entities;
using Taggy.Domain.Interfaces;
using Taggy.Infrastructure.Data;
 
namespace Taggy.Infrastructure.Repositories;
 
public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id) =>
        await context.Users.FindAsync(id);
 
    public async Task<User?> GetByEmailAsync(string email) =>
        await context.Users.FirstOrDefaultAsync(u => u.Email == email);
 
    public async Task<bool> ExistsByEmailAsync(string email) =>
        await context.Users.AnyAsync(u => u.Email == email);
 
    public async Task AddAsync(User user) =>
        await context.Users.AddAsync(user);
 
    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();

    public async Task<List<User>> GetAll()
    {
        return await context.Users.ToListAsync();
    }   
    
}
 