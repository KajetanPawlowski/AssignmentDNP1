using Domain.Model;

namespace Domain.DTOs;

public class PostCreationDTO
{
    public int AuthorId { get; }
    public string Title { get; }
    public string Body { get; }

    public PostCreationDTO(int userId, string title, string body)
    {
        AuthorId = userId;
        Title = title;
        Body = body;
    }
}