using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class PostLogic: IPostLogic
{
    private readonly IPostDAO postDao;

    public PostLogic(IPostDAO postdao)
    {
        this.postDao = postdao;
    }
    
    public Task<Post> CreateAsync(PostCreationDTO dto)
    {
        throw new NotImplementedException();
    }
}