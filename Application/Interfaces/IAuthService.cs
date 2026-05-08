using Taggy.Application.DTOs;

public interface IAuthService
{
    Task<AuthResponseDto> Register(RegisterDto registerDto);

    Task<AuthResponseDto> Login(LoginDto loginData);

    Task<GetMeResponseDto> GetMe(GetMeDto dto);
}