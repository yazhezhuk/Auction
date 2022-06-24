using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.GuardClauses;
using Auction.Core.Entities;
using Auction.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Auction.Client.Services;

public class AuthService : IAuthService
{
    private readonly ILogger<AuthService> _logger;
    
    private readonly UserManager<AuctionUser> _userManager;
    private readonly SignInManager<AuctionUser> _signInManager;
    
    private readonly IConfiguration _configuration;
    
    
    public AuthService(ILogger<AuthService> logger,
        UserManager<AuctionUser> userManager,
        SignInManager<AuctionUser> signInManager,
        IConfiguration configuration)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<AuctionUser> LogIn(string email, string password)
    {
        _logger.LogInformation("Trying to log in");
        var signInResult = await _signInManager.PasswordSignInAsync(email, password, false, false);

        if (!signInResult.Succeeded)
            throw new ArgumentException("Password or nickname are incorrect.Please try again.");

        _logger.LogInformation($"Login succeeded with email : {email}" +
                               $" and password : {password}");

        var user = await _userManager.FindByEmailAsync(email);
        await _userManager.AddClaimsAsync(user,new[]{
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user)).First())
        });
        return user;
    }

    public async Task SignIn(string email, string password, string firstname, string surname)
    {
        _logger.LogInformation("Trying to register new user");
        var existingUserModel = await _userManager.FindByEmailAsync(email);
        
        if (existingUserModel == null)
        {
            var user = new AuctionUser
            {
                Name = firstname,
                Surname = surname,
                Email = email,
                UserName = email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new ArgumentException("Error registering user.Please, try again.");
            await _userManager.AddToRolesAsync(user, new[] {"User"});
            _logger.LogInformation("User successfully registered");
        }

        _logger.LogInformation("Found user with this credentials.Throwing exception.");
        throw new NotFoundException("User","Such user already exist.Please, try another credentials.");
    }
    
    public async Task<string> GenerateJwtToken(AuctionUser appUser)
    {
        var keyString = _configuration.GetSection("SecretKey").Value;
        var encodedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

        var credentials = new SigningCredentials(encodedKey,
            SecurityAlgorithms.HmacSha256);

        var issuer = _configuration.GetSection("Issuer").Value;
        var audience = _configuration.GetSection("Audience").Value;
        var claims = await _userManager.GetClaimsAsync(appUser);

        
        var token = new JwtSecurityToken(issuer,
            audience,
            claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}