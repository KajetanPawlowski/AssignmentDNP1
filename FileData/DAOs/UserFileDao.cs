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
        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task DeleteAsync(string username)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
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
            u.Username.Equals(dto.Username, StringComparison.OrdinalIgnoreCase)
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
            u.Username.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }
// Get Users (param)
    public Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchParameters)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();
        if (searchParameters.UsernameContains != null)
        {
            users = context.Users.Where(u => u.Username.StartsWith(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }

    public async Task<User?> GetByIdAsync(int userId)
    {
        return context.Users.FirstOrDefault(u => u.UserId == userId);
    }
}