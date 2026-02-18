namespace PortFreight.Api.Controllers;

/// <summary>
/// Handles authentication-related HTTP requests, including user login and JWT token generation.
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<User> userManager, IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// Logins a user by validating their credentials and generating a JWT token if successful.
    /// </summary>
    /// <param name="dto">An object containing login details</param>
    /// <returns>An IActionResult that represents the result of the login operation.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null) 
            return Unauthorized();

        if (!await userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized();

        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Role, roles.First())
        };

        var claimRoles = roles.Select(role => new Claim(ClaimTypes.Role, role));
        claims.AddRange(claimRoles);

        var key = configuration["Jwt:Key"]!;
        var bytes = Encoding.UTF8.GetBytes(key);
        var symmetricSecurityKey = new SymmetricSecurityKey(bytes);
        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: credentials
        );

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(securityToken);
        return Ok(new { token });
    }

    /// <summary>
    /// Register a new user by creating an account with the provided details and assigning a default role.
    /// </summary>
    /// <param name="dto">An object containing register details</param>
    /// <returns>An IActionResult that represents the result of the register operation.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var existing = await userManager.FindByNameAsync(dto.Email);
        if (existing != null)
            return BadRequest("Email already exists");

        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.Username,
            Email = dto.Email
        };
        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, dto.Password);
        var result = await userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // Assign default role
        await userManager.AddToRoleAsync(user, RoleType.Unknown.GetDisplayName());

        return Ok("User registered successfully");
    }
}

