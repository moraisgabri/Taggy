using Taggy.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Taggy.Domain.Interfaces;
using Taggy.Domain.Entities;

namespace Taggy.Application.Services;

class UserService: IUserService
{
  private readonly IUserRepository userRepository;

  public UserService(IUserRepository _userRepository)
  
    {
      userRepository = _userRepository;
    }

  public async Task<UserDto> GetUserById(string id)
  {
    User user = await userRepository.GetByIdAsync(Guid.Parse(id)) ?? throw new Exception("User not found");
    return new UserDto
    {
        Id = user.Id.ToString(),
        Name = user.Name,
        Email = user.Email
    };
  }

  public async Task<UserDto> EditUser(EditUserDto editUserDto)
  {
    User user = await userRepository.GetByIdAsync(Guid.Parse(editUserDto.Id)) ?? throw new Exception("User not found");

    user.Name = editUserDto.Name ?? user.Name;
    user.Email = editUserDto.Email ?? user.Email;

    User? editedUser = await userRepository.Edit(user);
    
    if (editedUser == null)
        throw new Exception("User not found");

    return new UserDto
    {
        Id = editedUser.Id.ToString(),
        Name = editedUser.Name,
        Email = editedUser.Email
    };
  }
}