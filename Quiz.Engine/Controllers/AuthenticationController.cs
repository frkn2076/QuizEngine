using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Engine.Extensions;
using Quiz.Engine.Service;
using Quiz.Engine.Service.Contracts;
using Quiz.Engine.Service.DTOs;
using Quiz.Engine.ViewModels.Requests;

namespace Quiz.Engine.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(UserRequestModel request)
    {
        var userDTO = request.Adapt<UserDTO>();
        await _authenticationService.RegisterAsync(userDTO);
        return Ok();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInAsync(UserRequestModel request)
    {
        var userDTO = request.Adapt<UserDTO>();
        var response = await _authenticationService.SignInAsync(userDTO);
        return Ok(response);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("refresh")]
    public async Task<IActionResult> RefreshTokenAsync()
    {
        var isRefreshToken = HttpContext.User.IsRefreshToken();
        if (!isRefreshToken)
        {
            throw new RefreshTokenIsNotValidException();
        }

        var userId = HttpContext.User.GetUserId();
        var response = await _authenticationService.SignInByIdAsync(userId);
        return Ok(response);
    }
}