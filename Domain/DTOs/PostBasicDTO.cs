namespace Domain.DTOs;

public class PostBasicDTO
{
    public int PostId { get; }
    public string? UserName { get; }
    public string? Title { get; }
    public string? Body { get; }
    
    public DateTime Timestamp { get; set; }
    public PostBasicDTO(string? title, string? userName, int postId, string? body, DateTime dateTime)
    {
        Title = title;
        UserName = userName;
        PostId = postId;
        Body = body;
        Timestamp = dateTime;
    }
}