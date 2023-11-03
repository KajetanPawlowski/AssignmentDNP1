using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> RegisterUserAsync(UserCreationDTO dto);
    Task<User> ValidateUserAsync(UserLoginDTO dto);
    Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchParameters);

    
}