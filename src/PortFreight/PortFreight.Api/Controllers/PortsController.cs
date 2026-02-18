namespace PortFreight.Api.Controllers;

/// <summary>
/// Provides HTTP API endpoints for managing port records, including retrieving, creating, updating, and deleting ports.
/// </summary>
/// <remarks>This controller exposes CRUD operations for ports via RESTful endpoints. It relies on the
/// IPortService to interact with the underlying data store and returns appropriate HTTP responses based on the outcome
/// of each operation.</remarks>
/// <param name="service">The service used to perform operations related to ports, such as retrieving, creating, updating, and deleting port
/// data.</param>
[ApiController]
[Route("api/[controller]")]
public class PortsController(IPortService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all available ports asynchronously.
    /// </summary>
    /// <remarks>This method queries the underlying service for all ports and returns the results in a format
    /// suitable for API clients. If no ports are available, the response will indicate that no resources were
    /// found.</remarks>
    /// <returns>An IActionResult that contains a collection of port data transfer objects if any ports are found; otherwise, a
    /// NotFound result.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ports = (await service.GetAllPortsAsync()).ToList();
        return !ports.Any() 
            ? NotFound() 
            : Ok(ports.Select(port => port.ToDto()));
    }

    /// <summary>
    /// Retrieves the port information associated with the specified unique identifier.
    /// </summary>
    /// <remarks>This method asynchronously fetches the port details from the service. If the port does not
    /// exist, a 404 Not Found response is returned.</remarks>
    /// <param name="id">The unique identifier of the port to retrieve. Must be a valid GUID.</param>
    /// <returns>An IActionResult containing the port information as a DTO if found; otherwise, returns a NotFound result.</returns>
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var port = await service.GetPortAsync(id);
        return port is null 
            ? NotFound() 
            : Ok(port.ToDto());
    }

    /// <summary>
    /// Creates a new port using the specified data transfer object.
    /// </summary>
    /// <param name="dto">The data transfer object containing the details of the port to create. This parameter must not be null.</param>
    /// <returns>A result that indicates the outcome of the creation operation. If successful, returns a 201 Created response
    /// with the URI of the newly created port resource.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreatePortDto dto)
    {
        var entity = dto.ToPort();
        var created = await service.CreatePortAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToDto());
    }

    /// <summary>
    /// Updates the specified port with the provided data.
    /// </summary>
    /// <remarks>This method validates that the provided identifier matches the ID in the update data and that
    /// the port exists before applying updates.</remarks>
    /// <param name="id">The unique identifier of the port to update.</param>
    /// <param name="dto">An object containing the updated values for the port. The <see cref="UpdatePortDto.Id"/> property must match the
    /// specified <paramref name="id"/>.</param>
    /// <returns>A <see cref="IActionResult"/> that indicates the result of the operation. Returns <see cref="NoContentResult"/>
    /// if the update is successful; <see cref="BadRequestResult"/> if the identifiers do not match; or <see
    /// cref="NotFoundResult"/> if the port does not exist.</returns>
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, UpdatePortDto dto)
    {
        if (id != dto.Id) 
            return BadRequest("ID mismatch");

        var existing = await service.GetPortAsync(id);
        if (existing is null) 
            return NotFound();

        dto.MapToEntity(existing);
        await service.UpdatePortAsync(existing);

        return NoContent();
    }

    /// <summary>
    /// Deletes the port with the specified unique identifier.
    /// </summary>
    /// <remarks>This operation is asynchronous. Ensure that the provided identifier corresponds to an
    /// existing port before calling this method.</remarks>
    /// <param name="id">The unique identifier of the port to delete. Must be a valid <see cref="Guid"/>.</param>
    /// <returns>A <see cref="IActionResult"/> that indicates the result of the delete operation. Returns <see
    /// cref="NoContentResult"/> if the deletion is successful; otherwise, returns <see cref="NotFoundResult"/> if the
    /// port does not exist.</returns>
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await service.DeletePortAsync(id);
        return success ? NoContent() : NotFound();
    }
}