using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using wellmanage.clientapp.Shared.Interfaces;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Pages;

public partial class Login
{
    private LoginRequest loginModel = new LoginRequest();
    private bool loginSuccess = false;
    private bool loginFailed = false;

    [Inject]
    IAuthService authService { get; set; }
    [Inject]
    NavigationManager navigationManager { get; set; }

    [Inject]
    AuthenticationStateProvider authProvider { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var authState = await authProvider.GetAuthenticationStateAsync();
        if (authState.User.Identity?.IsAuthenticated == true)
        {
            navigationManager.NavigateTo("/home");
        }
    }
    private async Task HandleValidSubmit()
    {
      var value = await authService.Login(loginModel);
        if (value != null && value.AuthenticationToken != null)
        {
            navigationManager.NavigateTo("/home");
        }
    }
}