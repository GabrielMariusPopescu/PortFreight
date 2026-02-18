namespace PortFreight.Api.Controllers;

/// <summary>
/// Provides HTTP endpoints for managing user records, including retrieving, creating, and updating user
/// statuses.
/// </summary>
/// <remarks>This controller exposes endpoints for common user operations in the system. It supports
/// retrieving all users, fetching a specific user by its unique identifier, creating new users, and
/// updating the status of existing users. Each endpoint returns appropriate HTTP responses based on the outcome of
/// the operation.</remarks>
/// <param name="userManager">The service used to perform user-related operations, such as retrieving, creating, and updating user
/// records.</param>
[ApiController]
[Route("api/[controller]")]
public class UsersController(UserManager<User> userManager) : ControllerBase
{
    /// <summary>
    /// Retrieves all users as data transfer objects (DTOs).
    /// </summary>
    /// <remarks>This method asynchronously fetches all available users and returns them as DTOs. If no
    /// users are found, the response is a 404 Not Found. Use this endpoint to obtain a complete list of users
    /// for further processing or display.</remarks>
    /// <returns>A <see cref="IActionResult"/> containing a collection of shipment DTOs if any exist; otherwise, a 404 Not Found
    /// result.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userManager.Users.ToListAsync();
        var dtos = users.Select(user => user.ToDto());
        return !users.Any() ? NotFound() : Ok(dtos);
    }

    /// <summary>
    /// Retrieves the user details for the specified unique identifier.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to fetch the user. If no user exists
    /// for the specified identifier, the response is a 404 Not Found.</remarks>
    /// <param name="id">The unique identifier of the user to retrieve. Must be a valid <see cref="Guid"/>.</param>
    /// <returns>A <see cref="IActionResult"/> containing the user details if found; otherwise, a NotFound result.</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        var dto = user?.ToDto();
        return user == null ? NotFound() : Ok(dto);
    }
}
