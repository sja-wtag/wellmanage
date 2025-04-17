using wellmanage.clientapp.Shared.Models;

namespace wellmanage.clientapp.Shared.Pages;

public partial class Login
{
    private LoginModel loginModel = new();
    private string? ErrorMessage;

    private void HandleLogin()
    {
        // Replace with real authentication logic
        if (loginModel.Email == "admin@example.com" && loginModel.Password == "password")
        {
            ErrorMessage = null;
            // Redirect or set auth state
            Console.WriteLine("Login successful!");
        }
        else
        {
            ErrorMessage = "Invalid credentials. Please try again.";
        }
    }
}