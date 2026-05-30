using Microsoft.AspNetCore.Mvc;
using Taggy.Application.DTOs;
using Taggy.Application.Interfaces;
using Taggy.Application.Services;

namespace Taggy.API.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
  private readonly IUserService userService;

    public UserController(IUserService _userService)
    {
        userService = _userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
      try
      {
        return Ok(await userService.GetUserById(id));
      }
      catch (Exception err)
      {
        return BadRequest(new { message = err.Message });
      }
    }

    [HttpPut]
    public async Task<IActionResult> EditUser(EditUserDto editUserDto)
    {
      try
      {
        return Ok(await userService.EditUser(editUserDto));
      }
      catch (Exception err)
      {            
        return BadRequest(new { message = err.Message });
      }   
    }
}