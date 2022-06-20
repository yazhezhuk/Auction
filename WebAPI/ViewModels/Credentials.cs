namespace WebAPI.ViewModels;

public record RegisterCredentials(
    string Email,
    string FirstName,
    string Surname,
    string Password);
public record LogInCredentials(
    string Email,
    string Password);
