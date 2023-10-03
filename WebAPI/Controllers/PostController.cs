using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace ToDosAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic postLogic;

    public PostController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }
    //POST POST
    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(PostCreationDTO dto)
    {
        throw new NotImplementedException();
    }
    //GET Post
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? author, int aunthorId, string? title, string? body )
    {
        try
        {
            SearchPostParameterDTO parameters = new(author, aunthorId, title, body);
            IEnumerable<Post> posts= await postLogic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        } 
    }
}