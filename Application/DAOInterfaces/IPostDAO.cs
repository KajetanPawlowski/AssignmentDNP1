using Domain.DTOs;
using Domain.Model;

namespace Application.DAOInterfaces;

public interface IPostDAO
{
    Task<Post> CreateAsync(Post post);

    Task<List<Post>> GetAsync(int? userId, string? titleContent);
    Task UpdateAsync(PostUpdateDTO dto);
    Task<Post?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}