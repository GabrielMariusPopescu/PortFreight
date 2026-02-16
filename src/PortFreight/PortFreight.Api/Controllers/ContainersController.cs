namespace PortFreight.Api.Controllers;

/// <summary>
/// Handles HTTP requests for managing container resources, including retrieval, creation, updating, and deletion of
/// containers.
/// </summary>
/// <param name="service">The service used to perform operations on container resources.</param>
[ApiController]
[Route("api/[controller]")]
public class ContainersController(IContainerService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all container resources from the service.
    /// </summary>
    /// <remarks>This method asynchronously obtains all containers and returns them as DTOs. If no containers
    /// are available, the response is a 404 Not Found.</remarks>
    /// <returns>A <see cref="IActionResult"/> that contains a collection of container data transfer objects if any are found;
    /// otherwise, a NotFound result.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var containers = (await service.GetAllContainersAsync()).ToList();
        return !containers.Any() ? NotFound() : Ok(containers.Select(container => container.ToDto()));
    }

    /// <summary>
    /// Retrieves the container that matches the specified unique identifier.
    /// </summary>
    /// <remarks>This method asynchronously queries the underlying service for a container with the given
    /// identifier. If no container exists with the specified ID, a NotFound result is returned.</remarks>
    /// <param name="id">The unique identifier of the container to retrieve. Must be a valid <see cref="System.Guid"/>.</param>
    /// <returns>A <see cref="IActionResult"/> that represents the result of the operation. Returns a 200 OK response with the
    /// container data if found; otherwise, returns a 404 Not Found response.</returns>
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var container = await service.GetContainerAsync(id);
        return container is null ? NotFound() : Ok(container.ToDto());
    }

    /// <summary>
    /// Creates a new container and associates it with the specified shipment.
    /// </summary>
    /// <remarks>This method processes a POST request to create a container resource. The response includes
    /// the URI of the created container in the Location header.</remarks>
    /// <param name="shipmentId">The unique identifier of the shipment to which the new container will be associated.</param>
    /// <param name="dto">An object containing the details required to create the container.</param>
    /// <returns>An IActionResult that represents the result of the creation operation. Returns a 201 Created response with the
    /// location of the newly created container.</returns>
    [HttpPost("{shipmentId:Guid}")]
    public async Task<IActionResult> Create(Guid shipmentId, CreateContainerDto dto)
    {
        var container = dto.ToContainer(shipmentId);
        var created = await service.CreateContainerAsync(container);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToDto());
    }

    /// <summary>
    /// Updates the container with the specified identifier using the provided data.
    /// </summary>
    /// <remarks>If the <paramref name="id"/> does not match the <c>Id</c> property of <paramref name="dto"/>,
    /// the request is rejected with a bad request response. If no container exists with the specified ID, a not found
    /// response is returned.</remarks>
    /// <param name="id">The unique identifier of the container to update. Must match the ID in <paramref name="dto"/>.</param>
    /// <param name="dto">An object containing the updated data for the container. The <c>Id</c> property must match the <paramref
    /// name="id"/> parameter.</param>
    /// <returns>A <see cref="IActionResult"/> that indicates the result of the operation. Returns <see cref="NoContentResult"/>
    /// if the update is successful; otherwise, returns <see cref="BadRequestResult"/> if the IDs do not match, or <see
    /// cref="NotFoundResult"/> if the container does not exist.</returns>
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateContainerDto dto)
    {
        if (id != dto.Id) 
            return BadRequest("ID mismatch");

        var existing = await service.GetContainerAsync(id);
        if (existing is null) 
            return NotFound();

        dto.MapToEntity(existing);
        await service.UpdateContainerAsync(existing);

        return NoContent();
    }

    /// <summary>
    /// Deletes the container identified by the specified unique identifier.
    /// </summary>
    /// <remarks>This operation is asynchronous. Ensure that the provided identifier corresponds to an
    /// existing container to avoid a NotFound response.</remarks>
    /// <param name="id">The unique identifier of the container to delete.</param>
    /// <returns>An IActionResult that is NoContent if the container was successfully deleted; otherwise, NotFound if no
    /// container with the specified identifier exists.</returns>
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await service.DeleteContainerAsync(id);
        return success ? NoContent() : NotFound();
    }
}
