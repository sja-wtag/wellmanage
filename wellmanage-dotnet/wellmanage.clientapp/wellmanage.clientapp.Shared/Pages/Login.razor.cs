using Microsoft.AspNetCore.Components;
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

    private async Task HandleValidSubmit()
    {
      var value = await authService.Login(loginModel);
        if (value != null && value.AuthenticationToken != null)
        {
            navigationManager.NavigateTo("/home");
        }
    }
}