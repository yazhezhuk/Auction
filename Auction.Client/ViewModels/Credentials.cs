namespace WebAPI.ViewModels;

public record RegisterCredentials(
    string Email,
    string Password,
    string FirstName,
    string Surname);
public record LogInCredentials(
    string Email,
    string Password);
