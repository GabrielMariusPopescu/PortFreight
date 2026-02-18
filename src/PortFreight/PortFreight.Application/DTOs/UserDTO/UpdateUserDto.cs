namespace PortFreight.Application.DTOs.UserDTO;

public record UpdateUserDto(
        string FirstName,
        string LastName,
        string Username,
        string PhoneNumber);