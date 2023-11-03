namespace Domain.Model;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    //admin, user, owner
    public string Role { get; set; }
}