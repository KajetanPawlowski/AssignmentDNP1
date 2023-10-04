using Domain.DTOs;
using Domain.Model;

namespace Application.DAOInterfaces;

public interface IPostDAO
{
    Task<Post> CreateAsync(Post post);
    
    Task<IEnumerable<Post>> GetAsync(SearchPostParameterDTO searchParameters);
    Task UpdateAsync(Post post);
    Task<Post?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}