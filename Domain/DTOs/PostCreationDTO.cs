

namespace Domain.DTOs;

public class PostCreationDTO
{
    public int AuthorId { get; }
    public string Title { get; }
    public string Body { get; }
    
    public PostCreationDTO(string userid, string title, string body)
    {
        AuthorId = int.Parse(userid);
        Title = title;
        Body = body;
    }
}