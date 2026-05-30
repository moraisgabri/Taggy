using Taggy.Application.DTOs;

namespace Taggy.Application.Interfaces;

public interface IUserService
{
     Task<UserDto> GetUserById(string id);

    Task<UserDto> EditUser(EditUserDto editUserDto);
}