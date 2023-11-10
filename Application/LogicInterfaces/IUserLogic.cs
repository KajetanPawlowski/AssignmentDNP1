using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> RegisterUserAsync(UserLoginDTO dto);
    Task DeleteUserAsync(string username);
    Task<User> ValidateUserAsync(UserLoginDTO dto);
    Task<User> AssignRoleAsync(AssignRoleDTO dto);
    Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchParameters);

    
}