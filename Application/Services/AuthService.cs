using Taggy.Application.DTOs;
namespace Taggy.Application.Services;



public class AuthService
{
    // private readonly AppDbContext _context;

    public async Task<string> Register(RegisterDto registrationData)
    {
        return "Usuário criado com sucesso";
    }

    public async Task<string> Login(LoginDto loginData)
    {
        return "Usuário logado com sucesso";
    }
}