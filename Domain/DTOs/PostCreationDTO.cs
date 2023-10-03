
namespace Domain.DTOs;

public class PostCreationDTO
{
    public int UserId { get; }
    public string Title { get; }
    public string Body { get; }
    
    public PostCreationDTO(int userId, string title, string body)
    {
        UserId = userId;
        Title = title;
        Body = body;
    }
}