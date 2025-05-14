using System.ComponentModel.DataAnnotations;

namespace AuthServiceProvider.Models;

public class SignInFormData
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}
