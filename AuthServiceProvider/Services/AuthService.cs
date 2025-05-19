using AuthServiceProvider.Models;

namespace AuthServiceProvider.Services;

public interface IAuthService
{
    Task<SignInResult> SignInAsync(SignInFormData formData);
    Task<SignUpResult> SignUpAsync(SignUpFormData formData);
}

public class AuthService(AccountGrpcService.AccountGrpcServiceClient accountClient) : IAuthService
{
    private readonly AccountGrpcService.AccountGrpcServiceClient _accountClient = accountClient;

    public async Task<SignUpResult> SignUpAsync(SignUpFormData formData)
    {
        var request = new CreateAccountRequest
        {
            Email = formData.Email,
            Password = formData.Password
        };

        var response = await _accountClient.CreateAccountAsync(request);

        return response.Success
            ? new SignUpResult
            {
                Success = response.Success,
                Message = response.Message,
                UserId = response.UserId
            }
            : new SignUpResult
            {
                Success = response.Success,
                Message = response.Message
            };

    }

    public async Task<SignInResult> SignInAsync(SignInFormData formData)
    {
        var request = new ValidateCredentialsRequest
        {
            Email = formData.Email,
            Password = formData.Password
        };

        var response = await _accountClient.ValidateCredentialsAsync(request);

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
            UserId = response.UserId,
            AccessToken = null,
            RefreshToken = null
        };

    }
}
