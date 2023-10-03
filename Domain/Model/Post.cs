
namespace Domain.Model;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public User User { get; set; }
    public string Body { get; set; }
    
    public Post(User user, string title, string body)
    {
        User = user;
        Title = title;
        Body = body;
    }
}