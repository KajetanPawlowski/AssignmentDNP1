using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDTO dto);

    Task<IEnumerable<Post>> GetAsync(SearchPostParameterDTO searchParameters); 
}