using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auction.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.ViewModels;

namespace WebAPI.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    
    private readonly UserManager<AuctionUser> _userManager;
    private readonly SignInManager<AuctionUser> _signInManager;
    
    private readonly IConfiguration _configuration;
    
    public AuthController(
        ILogger<AuthController> logger,
        UserManager<AuctionUser> userManager,
        SignInManager<AuctionUser> signInManager,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("logIn")]
    public async Task<IActionResult> LogIn([FromBody] LogInCredentials loginModel)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false);

        if (!signInResult.Succeeded) return BadRequest("Password or nickname are incorrect.Please try again.");
        
        _logger.LogInformation($"Login succeeded with email : {loginModel.Email}" +
                               $" and password : {loginModel.Password}");
        
        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        await _userManager.AddClaimAsync(user, 
            new Claim(ClaimTypes.Name, user.UserName)
        );
        return Ok(new
        {
            Token = GenerateJwtToken(user)
        });
    }

    [HttpPost("signIn")]
    public async Task<IActionResult> SingIn([FromBody] RegisterCredentials registerModel)
    {
        _logger.LogInformation("Trying to register new user");
        var existingUserModel = await _userManager.FindByNameAsync(registerModel.Email);
        
        if (existingUserModel == null)
        {
            var user = new AuctionUser
            {
                Name = registerModel.FirstName,
                Surname = registerModel.Surname,
                Email = registerModel.Email,
                UserName = registerModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return BadRequest("Error registering user.Please, try again.");
            await _userManager.AddToRolesAsync(user, new[] {"User"});
            return Ok("User successfully registered");
        }

        _logger.LogInformation("Found user with this credentials.Returning bad request.");
        return BadRequest("Such user already exist.Please, try another credentials.");
    }

    private string GenerateJwtToken(AuctionUser appUser)
    {
        var keyString = _configuration.GetSection("SecretKey").Value;
        var encodedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

        var credentials = new SigningCredentials(encodedKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
            new Claim(ClaimTypes.Name, appUser.UserName)
        };
        
        var issuer = _configuration.GetSection("Issuer").Value;
        var audience = _configuration.GetSection("Audience").Value;

        var token = new JwtSecurityToken(issuer,
            audience,
            claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}