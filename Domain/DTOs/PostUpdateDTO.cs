namespace Domain.DTOs;

public class PostUpdateDTO
{
    public int PostId { get; }
    public int? UserId { get; set; } 
    public string? TitleContent { get; set; }
    public string? Body { get; set; }

    public PostUpdateDTO(int postId)  
    {
        PostId = postId;
        
    }
}