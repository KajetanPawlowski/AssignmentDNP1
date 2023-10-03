namespace Domain.Model;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; }
    public string Body { get; set; }

    public Post(int userId, string title, string body)
    {
        AuthorId = userId;
        Title = title;
        Body = body;
    }
}