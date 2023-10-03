using Application.DAOInterfaces;
using Domain.Model;

namespace FileData.DAOs;

public class PostFileDAO : IPostDAO
{
    public Task<Post> CreateAsync(Post post)
    {
        throw new NotImplementedException();
    }
}