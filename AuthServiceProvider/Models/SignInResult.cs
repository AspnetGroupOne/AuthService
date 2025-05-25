using Newtonsoft.Json;

namespace AuthServiceProvider.Models
{
    public class SignInResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? UserId { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
