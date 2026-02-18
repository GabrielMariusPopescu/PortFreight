namespace PortFreight.Application.DTOs.UserDTO;

public record CreateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    string PhoneNumber);