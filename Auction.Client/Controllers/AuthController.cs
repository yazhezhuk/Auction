using Auction.Client.Services;
using Auction.Core.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ViewModels;

namespace Auction.Client.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(AuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("logIn")]
    public async Task<IActionResult> LogIn([FromBody] LogInCredentials loginModel)
    {
        try
        {
            var authorizedUser = await _authService.LogIn(loginModel.Email, loginModel.Password);
            var token = _authService.GenerateJwtToken(authorizedUser);
            
            _logger.LogInformation($"Log in success with token: {token}");
            
            return Ok(token);
        }
        catch (ArgumentException e)
        {
            _logger.LogInformation($"Log in failed: {e.Message}");
            
            return BadRequest(e.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<IActionResult> SingIn([FromBody] RegisterCredentials registerModel)
    {
        try
        {
            _authService.SignIn(registerModel.Email
                ,registerModel.Password,
                registerModel.Surname,
                registerModel.FirstName);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}