using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    //admin, user, owner
    public string Role { get; set; }
    
    public ICollection<Post> Posts { get; set; }
}