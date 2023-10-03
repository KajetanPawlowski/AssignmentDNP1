using Application.DAOInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class UserLogic :IUserLogic
{
    private readonly IUserDAO userDao;

    public UserLogic(IUserDAO userDao)
    {
        this.userDao = userDao;
    }
    public async Task<User> CreateAsync(UserCreationDTO dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateUsernameData(dto);
        User toCreate = new User 
        {
            UserName = dto.UserName
        };
    
        User created = await userDao.CreateAsync(toCreate);
    
        return created;
    }
    
    private static void ValidateUsernameData(UserCreationDTO userToCreate)
    {
        string userName = userToCreate.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
    public Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }
}