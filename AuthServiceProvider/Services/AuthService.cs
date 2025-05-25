using AuthServiceProvider.DTOs;
using AuthServiceProvider.Models;
using Newtonsoft.Json;

namespace AuthServiceProvider.Services;

public interface IAuthService
{
    Task<SignInResult> SignInAsync(SignInFormData formData);
    Task<SignUpResult> SignUpAsync(SignUpFormData formData);
}

public class AuthService: IAuthService
{
    public async Task<SignUpResult> SignUpAsync(SignUpFormData formData)
    {
        try
        {
            var request = new SignUpFormData
            {
                Email = formData.Email,
                Password = formData.Password
            };

            using var http = new HttpClient();
            var result = await http.PostAsJsonAsync("https://ventixeaccountserviceprovider-ejd0hpged4f6enb2.swedencentral-01.azurewebsites.net/api/Accounts/Create", request);
            var response = JsonConvert.DeserializeObject<SignUpResult>(await result.Content.ReadAsStringAsync());

            return response == null
                ? throw new Exception("response is null")
                : response.Success
                ? new SignUpResult
                {
                    Success = response.Success,
                    Message = response.Message,
                    UserId = formData.Email
                }
                : new SignUpResult
                {
                    Success = response.Success,
                    Message = response.Message
                };
        }
        catch (Exception ex)
        {
            return new SignUpResult
                {
                    Success = false,
                    Message = ex.Message
                };
        }

    }

    public async Task<SignInResult> SignInAsync(SignInFormData formData)
    {
        try
        {
            var request = new SignInFormData
            {
                Email = formData.Email,
                Password = formData.Password
            };

            using var http = new HttpClient();
            var result = await http.PostAsJsonAsync("https://ventixeaccountserviceprovider-ejd0hpged4f6enb2.swedencentral-01.azurewebsites.net/accounts/Validate", request);
            var response = JsonConvert.DeserializeObject<SignInResult>(await result.Content.ReadAsStringAsync());

            if (response == null)
            {
                return new SignInResult
                {
                    Success = false,
                    Message = "Failed to deserialize response from account service."
                };
            }

            if (!response.Success)
            {
                return new SignInResult
                {
                    Success = response.Success,
                    Message = response.Message,
                };
            }



            return new SignInResult
            {
                Success = response.Success,
                Message = response.Message,
                UserId = formData.Email,
                AccessToken = null,
                RefreshToken = null
            };
        }

        catch (Exception ex)
        {
            return new SignInResult
            {
                Success = false,
                Message = ex.Message
            };
        }

    }
}
