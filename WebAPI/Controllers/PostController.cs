using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic postLogic;

    public PostController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }
    //POST Post
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync(PostCreationDTO dto)
    {
        try
        {
            Post post = await postLogic.CreateAsync(dto);
            return Created($"/posts/{post.PostId}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    //GET Post
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetByUsernameAsync([FromQuery] string? userName, [FromQuery] string? titleContent)
    {
        try
        {
            SearchPostParameterDTO parameters = new(userName, titleContent);
            IEnumerable<Post> posts= await postLogic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        } 
    }
    //PATCH Post
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] PostUpdateDTO post)
    {
        try
        {
            await postLogic.UpdateAsync(post);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    //GET BY ID Post
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostBasicDTO>> GetById([FromRoute] int id)
    {
        try
        {
            PostBasicDTO result = await postLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
  //DELETE Post
  /*[HttpGet("{id:int}")]
  public async Task<ActionResult> DeleteAsync([FromRoute] int id)
  {
      try
      {
          await postLogic.DeleteAsync(id);
          return Ok();
      }
      catch (Exception e)
      {
          Console.WriteLine(e);
          return StatusCode(500, e.Message);
      }
  }*/
}