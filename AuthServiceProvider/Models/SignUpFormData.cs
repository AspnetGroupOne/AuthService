using System.ComponentModel.DataAnnotations;

namespace AuthServiceProvider.Models;

public class SignUpFormData
{
    [Required]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;

    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Invalid Password")]
    public string Password { get; set; } = null!;
}
