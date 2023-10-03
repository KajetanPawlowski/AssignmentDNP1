namespace Domain.Model;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public User Author { get; }
    public string Body { get; set; }
    public Post(User author, string title, string body)
    {
        Author = author;
        Title = title;
        Body = body;
    }
}