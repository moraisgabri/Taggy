using System.IdentityModel.Tokens.Jwt;
using Taggy.Application.DTOs;
using Taggy.Domain.Entities;
using Taggy.Domain.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Taggy.Application.Services;

public class AuthService(IUserRepository _userRepository, IConfiguration _configuration): IAuthService
{
    private readonly IUserRepository userRepository = _userRepository;
    private readonly IConfiguration configuration = _configuration;

    public async Task<AuthResponseDto> Register(RegisterDto registerDto)
    {
        if (await userRepository.ExistsByEmailAsync(registerDto.Email))
            throw new InvalidOperationException("Email already in use.");

        var user = new User
        {
            Id        = Guid.NewGuid(),
            Name      = registerDto.Name,
            Email     = registerDto.Email.ToLowerInvariant(),
            Password  = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
            CreatedAt = DateTime.UtcNow
        };

        await userRepository.AddAsync(user);
        await userRepository.SaveChangesAsync();

        return new AuthResponseDto{
            Id = user.Id.ToString(),
            Name = user.Name,
            Token = GenerateToken(user),
            Email = user.Email
        };
    }

    public async Task<AuthResponseDto> Login(LoginDto loginData)
    {
        User user = await userRepository.GetByEmailAsync(loginData.Email.ToLowerInvariant())
        ?? throw new UnauthorizedAccessException("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(loginData.Password, user.Password))
        throw new UnauthorizedAccessException("Invalid credentials.");

        return new AuthResponseDto{
            Id = user.Id.ToString(),
            Name = user.Name, 
            Email = user.Email,
            Token = GenerateToken(user)
        };
    }

    private string GenerateToken(User user)
    {
        var jwtConfig = configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Secret"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Name, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtConfig["Issuer"],
            audience: jwtConfig["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}