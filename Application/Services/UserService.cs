using Taggy.Application.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Taggy.Infrastructure.Repositories;
using Taggy.Domain.Interfaces;
using Taggy.Domain.Entities;

namespace Taggy.Application.Services;

class UserService: IUserService
{
  private readonly IUserRepository userRepository;

  public UserService(UserRepository _userRepository)
  
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
    User updatedUser = new User
    {
        Id = Guid.Parse(editUserDto.Id),
        Name = editUserDto.Name,
        Email = editUserDto.Email,
        Password = editUserDto.Password
    };

    User editedUser = await userRepository.Edit(updatedUser);
    
    return new UserDto
    {
        Id = editedUser.Id.ToString(),
        Name = editedUser.Name,
        Email = editedUser.Email
    };
  }
}