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
        post.Timestamp = DateTime.Now;

        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public async Task<List<Post>> GetAsync(int? userId, string? titleContent) 
    {
        IQueryable<Post> query = context.Posts.AsQueryable();

        if (userId != null)
        {
            query = query.Where(post => post.User.UserId == userId);
        }

        if (!string.IsNullOrEmpty(titleContent))
        {
            query = query.Where(post =>
                post.Title.Contains(titleContent, StringComparison.OrdinalIgnoreCase));
        }

        List<Post> result = query.ToList();

        return result;
    }
    public Task UpdateAsync(PostUpdateDTO dto)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.PostId == dto.PostId);
        if (existing == null)
        {
            throw new Exception($"Post with id {dto.PostId} does not exist!");
        }
        if (dto.NewTitle != null)
        {
            existing.Title = dto.NewTitle;
        }
        if (dto.NewBody != null)
        {
            existing.Body = dto.NewBody;
        }

        existing.Timestamp = DateTime.Now;
        
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