using System.Security.Claims;
using Domain.DTOs;

namespace HttpClients.Interfaces;

public interface IAuthHttpClient
{
    public Task LoginAsync(UserLoginDTO dto);
    public Task LogoutAsync();
    public Task RegisterAsync(UserLoginDTO dto);
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}