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
    public async Task<User> RegisterUserAsync(UserCreationDTO dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateRegistrationData(dto);
        User toCreate = new User 
        {
            UserName = dto.UserName,
            Password = dto.Password,
            Role = dto.Role
        };
    
        User created = await userDao.CreateAsync(toCreate);
    
        return created;
    }

    public async Task<User> ValidateUserAsync(UserLoginDTO dto)
    {
        User? existingUser = await userDao.GetByUsernameAsync(dto.Username);
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(dto.Password))
        {
            throw new Exception("Password mismatch");
        }

        return await Task.FromResult(existingUser);
    }

    private static void ValidateRegistrationData(UserCreationDTO userToCreate)
    {
        string userName = userToCreate.UserName;
        string password = userToCreate.Password;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        if (password.Length < 3)
            throw new Exception("Password must be at least 3 characters!");

        if (password.Length > 15)
            throw new Exception("Password must be less than 16 characters!");
    }
    public Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }
 
}