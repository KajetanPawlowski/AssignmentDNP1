namespace Domain.DTOs;

public class PostBasicDTO
{
    public int PostId { get; }
    public string? UserName { get; }
    public string? Title { get; }
    public string? Body { get; }
    
    public PostBasicDTO(string? title, string? userName, int postId, string? body)
    {
        Title = title;
        UserName = userName;
        PostId = postId;
        Body = body;
    }
}