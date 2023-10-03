using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class PostLogic: IPostLogic
{
    private readonly IPostDAO postDao;
    private readonly IUserDAO userDao;

    public PostLogic(IPostDAO postdao, IUserDAO userdao)
    {
        this.postDao = postdao;
        this.userDao = userdao;
    }
    
    public async Task<Post> CreateAsync(PostCreationDTO dto)
    {
        User? author = await userDao.GetByIdAsync(dto.AuthorId);
        if (author == null)
            throw new Exception("UserNotFound");
        
        ValidatePostCreationData(dto);
        Post toCreate = new Post(author, dto.Title, dto.Body);

        Post created = await postDao.CreateAsync(toCreate);

        return created;
    }

    private static void ValidatePostCreationData(PostCreationDTO dto)
    {
        string titleWithoutBlanks = dto.Title.Replace(" ", "");
        string body = dto.Body;

        if (titleWithoutBlanks.Length == 0)
        {
            throw new Exception("Post has to have a title");
        }

        if (body.Length > 350)
        {
            throw new Exception("Post can be maximum 350 characters");
        }
    }
    
    public Task<IEnumerable<Post>> GetAsync(SearchPostParameterDTO searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }
}