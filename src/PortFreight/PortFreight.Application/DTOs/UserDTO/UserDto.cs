namespace PortFreight.Application.DTOs.UserDTO;

public record UserDto(
    Guid Id, 
    string FirstName, 
    string LastName, 
    string Email, 
    bool IsEmailConfirmed, 
    string Username, 
    string PhoneNumber, 
    bool IsPhoneNumberConfirmed);