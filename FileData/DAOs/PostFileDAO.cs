using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace FileData.DAOs;

public class PostFileDAO : IPostDAO
{
    private readonly FileContext context;

    public PostFileDAO(FileContext context)
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
        
        if (!string.IsNullOrEmpty(searchParameters.Author))
        {
            result = context.Posts.Where(todo =>
                todo.User.UserName.Equals(searchParameters.Author, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParameters.AuthorId != null)
        {
            result = result.Where(t => t.User.Id == searchParameters.AuthorId);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.Title))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParameters.Title, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }
}