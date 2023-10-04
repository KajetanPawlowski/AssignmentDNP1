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
        ValidatePostCreationData(dto);
        
        User? user = await userDao.GetByIdAsync(dto.UserId);
        if (user == null)
            throw new Exception("UserNotFound");
        
        Post toCreate = new Post(user, dto.Title, dto.Body);

        Post created = await postDao.CreateAsync(toCreate);

        return created;
    }

    private static void ValidatePostCreationData(PostCreationDTO dto)
    {
        string title = dto.Title;
        string body = dto.Body;

        if (string.IsNullOrEmpty(title))
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

    public async Task UpdateAsync(PostUpdateDTO post)
    {
        Post? existing = await postDao.GetByIdAsync(post.PostId);
        
        if (existing == null)
        {
            throw new Exception($"Post with ID {post.PostId} not found!");
        }

        User? user = null;
        if (post.UserId != null)
        {
            user = await userDao.GetByIdAsync((int)post.UserId);
            if (user == null)
            {
                throw new Exception($"User with id {post.UserId} was not found.");
            }
        }

        User userToUse = user ?? existing.User;
        string titleToUse = post.TitleContent ?? existing.Title;
        string bodyToUse = post.Body ?? existing.Body;

        Post updated = new(userToUse, titleToUse, bodyToUse)
        {
            Title = titleToUse,
            Body  = bodyToUse,
            PostId = existing.PostId
        };

        await postDao.UpdateAsync(updated);
    }

    public async Task<PostBasicDTO> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return new PostBasicDTO(post.Title, post.User.UserName, post.PostId, post.Body);
    }

    public async Task DeleteAsync(int id)
    {
        Post? todo = await postDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Post with ID {id} was not found!");
        }
        
        await postDao.DeleteAsync(id);
    }
}