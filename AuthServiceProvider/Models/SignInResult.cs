using Newtonsoft.Json;

namespace AuthServiceProvider.Models
{
    public class SignInResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("userId")]
        public string? UserId { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
