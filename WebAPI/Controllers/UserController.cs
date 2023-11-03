using Application.LogicInterfaces;
using Domain;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    // Post User
    [HttpPost, Authorize(Policy = "isAdmin")]

    public async Task<ActionResult<User>> CreateAsync(UserCreationDTO dto)
    {
        try
        {
            User user = await userLogic.RegisterUserAsync(dto);
            return Created($"/users/{user.Id}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    //GET User
    [HttpGet, Authorize(Policy = "isUser")]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username)
    {
        try
        {
            SearchUserParameterDTO parameters = new(username);
            IEnumerable<User> users = await userLogic.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}