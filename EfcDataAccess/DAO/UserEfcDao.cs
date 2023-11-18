using Application.DAOInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAO;

public class UserEfcDao : IUserDAO
{
    private readonly GossipsDbContext context;
    
    public UserEfcDao(GossipsDbContext context)
    {
        this.context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }

    public async Task DeleteAsync(string username)
    {
        User? user = await GetByUsernameAsync(username);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }

    public async Task<User> AssignRoleAsync(AssignRoleDTO dto)
    {
        User? user = await GetByUsernameAsync(dto.Username);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        user.Role = dto.Role;
        context.Users.Update(user);
        
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        User? existing = await context.Users.FirstOrDefaultAsync(u =>
            u.Username.Equals(userName)
        );
        return existing;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParameterDTO searchParameters)
    {
        IQueryable<User> usersQuery = context.Users.AsQueryable();
        if (searchParameters.UsernameContains != null)
        {
            usersQuery = usersQuery.Where(u => u.Username.Contains(searchParameters.UsernameContains));
        }

        IEnumerable<User> result = await usersQuery.ToListAsync();
        return result;
    }

    public async Task<User?> GetByIdAsync(int dtoOwnerId)
    {
        User? user = await context.Users.FindAsync(dtoOwnerId);
        return user;
    }
}