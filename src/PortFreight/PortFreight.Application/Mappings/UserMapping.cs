namespace PortFreight.Application.Mappings;

public static class UserMapping
{
    public static UserDto ToDto(this User user) =>
        new(user.Id.ToGuid(), 
            user.FirstName, 
            user.LastName, 
            user.Email!, 
            user.EmailConfirmed, 
            user.UserName!, 
            user.PhoneNumber!, 
            user.PhoneNumberConfirmed);

    public static User ToUser(this UserDto dto) =>
        new()
        {
            Id = dto.Id.ToString(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.Username,
            Email = dto.Email,
            EmailConfirmed = dto.IsEmailConfirmed,
            PhoneNumber = dto.PhoneNumber,
            PhoneNumberConfirmed = dto.IsPhoneNumberConfirmed
        };

    public static User ToUser(this CreateUserDto dto) =>
        new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Username,
            PhoneNumber = dto.PhoneNumber
        };
}