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

        return Task.FromResult(result);
    }

    public Task UpdateAsync(Post toUpdate)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.PostId == toUpdate.PostId);
        if (existing == null)
        {
            throw new Exception($"Post with id {toUpdate.PostId} does not exist!");
        }

        context.Posts.Remove(existing);
        context.Posts.Add(toUpdate);
        
        context.SaveChanges();
        
        return Task.CompletedTask;
    }

    public Task<Post?> GetByIdAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(t => t.PostId == id);
        return Task.FromResult(existing); 
    }
    
    public Task DeleteAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.PostId == id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {id} does not exist!");
        }

        context.Posts.Remove(existing); 
        context.SaveChanges();
    
        return Task.CompletedTask;
    }
}