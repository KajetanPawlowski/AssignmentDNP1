using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDTO dto);
    Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchParameters);

    
}