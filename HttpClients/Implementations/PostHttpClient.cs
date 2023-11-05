using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Model;
using HttpClients.Interfaces;

namespace HttpClients.Implementations;

public class PostHttpClient : IPostHttpClient
{
    private readonly HttpClient client;

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }
    public async Task AddPost(string username, string title, string body)
    {
        
        PostCreationDTO dto = new()
        {
            Username = username,
            Title = title,
            Body = body
        };
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthClient.Jwt);
        
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent content = new(dtoAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsJsonAsync("/Post", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Post post = JsonSerializer.Deserialize<Post>(result)!;
        Console.WriteLine(post);
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthClient.Jwt);
        HttpResponseMessage response = await client.GetAsync("/Post");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response +"");
        }

        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        posts.Reverse();
        return posts;
    }

    public async Task<List<Post>> GetUserPostsAsync(string username)
    {
        SearchPostParameterDTO dto = new()
        {
            UserName = username
        };
        string uri = "/Post";
        string query = ConstructQuery(dto);
        HttpResponseMessage response = await client.GetAsync(uri+query);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        posts.Reverse();
        return posts;
    }
    private static string ConstructQuery(SearchPostParameterDTO dto)
    {
        string query = "";
        if (!string.IsNullOrEmpty(dto.UserName))
        {
            query += $"?userName={dto.UserName}";
        }
        if (!string.IsNullOrEmpty(dto.TitleContent))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"titleContent={dto.TitleContent}";
        }

        return query;
    }
}