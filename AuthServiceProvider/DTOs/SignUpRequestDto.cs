namespace AuthServiceProvider.DTOs;

public class SignUpRequestModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class ValidateCredentialsResponseModel
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? UserId { get; set; }
}