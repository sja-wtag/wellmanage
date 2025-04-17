using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using wellmanage.shared.Enums;


namespace wellmanage.domain.Entity;

public class User : IdentityUser<long>
{
    [Required]
    public required string FullName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public StatusEnum Status { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}