namespace Domain.DTOs;

public class UserCreationDTO
{
    public string UserName { get;}

    public UserCreationDTO(string userName)
    {
        UserName = userName;
    }
}