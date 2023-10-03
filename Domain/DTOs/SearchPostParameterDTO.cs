namespace Domain.DTOs;

public class SearchPostParameterDTO
{
    public string? UserName { get;}
    public string? TitleContent { get;}
    
    public string? BodyContent { get;}
    
    public SearchPostParameterDTO(string? userName, string? titleContent, string? bodyContent)
    {
        UserName = userName;
        TitleContent = titleContent;
        BodyContent = bodyContent;
    }
}