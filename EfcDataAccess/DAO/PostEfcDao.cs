using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAO;

public class PostEfcDao : IPostDAO
{
    private readonly GossipsDbContext context;
    
    public PostEfcDao(GossipsDbContext context)
    {
        this.context = context;
    }
    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return added.Entity;
    }

    public Task<List<Post>> GetAsync(int? userId, string? titleContent)
    {
        IQueryable<Post> query = context.Posts.AsQueryable();
        if (userId != null)
        {
            query = query.Where(post =>
                post.User.UserId == userId);
        }

        if (!string.IsNullOrEmpty(titleContent))
        {
            query = query.Where(post =>
                post.Title.ToLower().Contains(titleContent));
        }

        List<Post> result = query.ToList();

        return Task.FromResult(result);
    }

    public async Task UpdateAsync(PostUpdateDTO dto)
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

        context.Update(existing);
        await context.SaveChangesAsync();
    }

    public Task<Post?> GetByIdAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(t => t.PostId == id);
        return Task.FromResult(existing);
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.PostId == id);
        if (existing == null)
        {
            throw new Exception($"Todo with id {id} does not exist!");
        }

        context.Posts.Remove(existing); 
        await context.SaveChangesAsync();
    }
}