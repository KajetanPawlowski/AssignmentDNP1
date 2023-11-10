using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace FileData.DAOs;

public class UserFileDao : IUserDAO
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context;
    } 
// Post User
    public Task<User> CreateAsync(User user)
    {
        int userId = 0;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task DeleteAsync(string username)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
        );
        if(existing != null )
        {
            context.Users.Remove(existing);
            context.SaveChanges();
        }
        return Task.CompletedTask;
    }

    public Task<User> AssignRoleAsync(AssignRoleDTO dto)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(dto.Username, StringComparison.OrdinalIgnoreCase)
        );
        ValidateRole(dto.Role);
        
        existing.Role = dto.Role;
        
        context.SaveChanges();
        
        return Task.FromResult(existing);
    }

    private void ValidateRole(string role)
    {
        switch (role)
        {
            case "admin":
            case "user":
                // Valid roles, no exception thrown
                break;

            default:
                throw new Exception("Invalid role");
        }
    }


    public Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }
// Get Users (param)
    public Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u => u.UserName.StartsWith(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }

    public Task<User?> GetByIdAsync(int dtoOwnerId)
    {
        User? result = context.Users.FirstOrDefault(u => u.Id == dtoOwnerId);
        
        return Task.FromResult(result);
    }
}