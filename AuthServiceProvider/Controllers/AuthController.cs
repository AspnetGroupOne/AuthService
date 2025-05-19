using AuthServiceProvider.Models;
using AuthServiceProvider.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServiceProvider.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpFormData formData)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _authService.SignUpAsync(formData);
        return result.Success 
            ? Ok(result) 
            : Problem(result.Message);
    }


    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInFormData formData)
    {
        if (!ModelState.IsValid)
        {
            return Unauthorized("Invalid login");
        }

        var result = await _authService.SignInAsync(formData);
        return result.Success
            ? Ok(result)
            : Unauthorized(result.Message);
    }
}
