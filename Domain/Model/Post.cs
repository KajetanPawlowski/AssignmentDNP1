
namespace Domain.Model;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Username { get; set; }
    public string Body { get; set; }
    
    public DateTime Timestamp { get; set; }
    public Post(string username, string title, string body)
    {
        Username = username;
        Title = title;
        Body = body;
    }
}