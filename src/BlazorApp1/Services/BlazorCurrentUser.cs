using System.Security.Claims;
using CraftBoard.Application.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp1.Services;

public class BlazorCurrentUser(AuthenticationStateProvider authenticationStateProvider) : ICurrentUser
{
    public async Task<string> GetRequiredUserIdAsync()
    {
        var state = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = state.User;

        if (user.Identity?.IsAuthenticated != true)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new UnauthorizedAccessException("User ID not found.");
        }
        return userId;
    }
}
