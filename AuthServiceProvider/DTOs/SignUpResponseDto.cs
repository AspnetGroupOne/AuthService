namespace AuthServiceProvider.DTOs;

public class SignUpResponseDto
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string UserId { get; set; } = null!;
    public string Email { get; set; } = null!;
}
