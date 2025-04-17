using System.ComponentModel.DataAnnotations;

namespace wellmanage.clientapp.Shared.Models;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}