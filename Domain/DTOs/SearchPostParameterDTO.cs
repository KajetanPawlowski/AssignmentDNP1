namespace Domain.DTOs;

public class SearchPostParameterDTO
{
    public string? Author { get;}
    public int? AuthorId { get;}
    public string? Title{ get;}
    public string? Body { get; }

    public SearchPostParameterDTO(string? user, int? userId, string? title, string body)
    {
        Author = user;
        AuthorId = userId;
        Title = title;
        Body = body;
    }
}