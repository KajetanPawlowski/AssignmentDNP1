using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace FileData.DAOs;

public class PostFileDao : IPostDAO
{
    private readonly FileContext context;

    public PostFileDao(FileContext context)
    {
        this.context = context;
    }  
    public Task<Post> CreateAsync(Post post)
    {   
        int postId = 0;
        if (context.Posts.Any())
        {
             postId = context.Posts.Max(p => p.PostId);
             postId++;
        }

        post.PostId = postId;

        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParameterDTO searchParameters)
    {
        IEnumerable<Post> result = context.Posts.AsEnumerable();
        
        if (!string.IsNullOrEmpty(searchParameters.UserName))
        {
            result = context.Posts.Where(post =>
                post.User.UserName.Equals(searchParameters.UserName, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrEmpty(searchParameters.TitleContent))
        {
            result = context.Posts.Where(post =>
                post.Title.Contains(searchParameters.TitleContent, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrEmpty(searchParameters.BodyContent))
        {
            result = context.Posts.Where(post =>
                post.Body.Contains(searchParameters.BodyContent, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }
    
    
}