using Domain.DTOs;
using Domain.Model;

namespace Application.DAOInterfaces;

public interface IUserDAO
{
    Task<User> CreateAsync(User user);
    Task DeleteAsync(string username);
    Task<User> AssignRoleAsync(AssignRoleDTO dto);
    Task<User?> GetByUsernameAsync(string userName);
    Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchParameters);
    Task<User?> GetByIdAsync(int dtoOwnerId);
}
