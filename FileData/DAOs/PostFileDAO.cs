using Application.DAOInterfaces;
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
            postId = context.Posts.Max(p => p.Id);
            postId++;
        }

        post.Id = postId;

        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }
}