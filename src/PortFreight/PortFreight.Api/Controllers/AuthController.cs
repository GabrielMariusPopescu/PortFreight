namespace PortFreight.Api.Controllers;

/// <summary>
/// Handles authentication-related HTTP requests, including user login and JWT token generation.
/// </summary>
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<User> userManager, TokenService tokenService, IdentityDatabaseContext identityContext, IConfiguration configuration) : ControllerBase
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
            new(ClaimTypes.Name, user.UserName!),
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

    /// <summary>
    /// Refreshes a JWT token by validating the provided refresh token, rotating it, and issuing a new JWT if valid.
    /// </summary>
    /// <param name="dto">An object containing refresh token details</param>
    /// <returns>An IActionResult that represents the result of the refresh operation.</returns>
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest dto)
    {
        var stored = await identityContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == dto.RefreshToken);

        if (stored == null || stored.Revoked || stored.ExpiresAt < DateTime.UtcNow)
            return Unauthorized("Invalid refresh token");

        // Rotate token
        stored.Revoked = true;

        var newRefresh = new RefreshToken
        {
            Token = tokenService.GenerateRefreshToken(),
            UserId = stored.UserId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        identityContext.RefreshTokens.Add(newRefresh);
        await identityContext.SaveChangesAsync();

        // Issue new JWT
        var roles = await userManager.GetRolesAsync(stored.User);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, stored.User.UserName!),
            new Claim(ClaimTypes.NameIdentifier, stored.User.Id)
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var key = configuration["Jwt:Key"]!;
        var bytes = Encoding.UTF8.GetBytes(key);
        var symmetricSecurityKey = new SymmetricSecurityKey(bytes);
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: signingCredentials
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(jwt),
            refreshToken = newRefresh.Token
        });
    }
}

