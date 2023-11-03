namespace Domain.DTOs;

public class UserCreationDTO
{
    public string UserName { get; }
    public string Password { get; }
    //admin, user, owner
    public string Role { get; }

    public UserCreationDTO(string userName, string password, string role)
    {
        UserName = userName;
        Password = password;
        Role = role;
    }
}