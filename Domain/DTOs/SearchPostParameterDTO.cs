namespace Domain.DTOs;

public class SearchPostParameterDTO
{
    public string? UserName { get;}
    public string? TitleContent { get;}
    
    public SearchPostParameterDTO(string? userName, string? titleContent)
    {
        UserName = userName;
        TitleContent = titleContent;
    }
}