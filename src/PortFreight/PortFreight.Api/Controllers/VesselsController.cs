namespace PortFreight.Api.Controllers;

/// <summary>
/// Provides API endpoints for managing vessels.
/// </summary>
/// <remarks>
/// Allows clients to create, retrieve, update, and delete vessel records.
/// </remarks>
[ApiController]
[Route("api/[controller]")]
public class VesselsController(IVesselService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all vessels.
    /// </summary>
    /// <returns>
    /// 200 OK with a collection of vessels if any exist;  
    /// 404 Not Found if no vessels are available.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var vessels = await service.GetAllVesselsAsync();
        return !vessels.Any()
            ? NotFound()
            : Ok(vessels.Select(vessel => vessel.ToDto()));
    }

    /// <summary>
    /// Retrieves a specific vessel by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier (GUID) of the vessel.</param>
    /// <returns>
    /// 200 OK with the requested vessel if found;  
    /// 404 Not Found if the vessel does not exist.
    /// </returns>
    [HttpGet("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var vessel = await service.GetVesselAsync(id);
        return vessel is null
            ? NotFound()
            : Ok(vessel.ToDto());
    }

    /// <summary>
    /// Creates a new vessel.
    /// </summary>
    /// <param name="dto">The data transfer object containing vessel details.</param>
    /// <returns>
    /// 201 Created with the newly created vessel;  
    /// 400 Bad Request if the input is invalid.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateVesselDto dto)
    {
        var entity = dto.ToVessel();
        var created = await service.CreateVesselAsync(entity);

        return CreatedAtAction(
            nameof(Get),
            new { id = created.Id },
            created.ToDto());
    }

    /// <summary>
    /// Updates an existing vessel.
    /// </summary>
    /// <param name="id">The unique identifier (GUID) of the vessel to update.</param>
    /// <param name="dto">The updated vessel data.</param>
    /// <returns>
    /// 204 No Content if the update is successful;  
    /// 400 Bad Request if the route ID does not match the DTO ID;  
    /// 404 Not Found if the vessel does not exist.
    /// </returns>
    [HttpPut("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, UpdateVesselDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID mismatch");

        var existing = await service.GetVesselAsync(id);
        if (existing is null)
            return NotFound();

        dto.MapToEntity(existing);
        await service.UpdateVesselAsync(existing);

        return NoContent();
    }

    /// <summary>
    /// Deletes a vessel by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier (GUID) of the vessel to delete.</param>
    /// <returns>
    /// 204 No Content if deletion is successful;  
    /// 404 Not Found if the vessel does not exist.
    /// </returns>
    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await service.DeleteVesselAsync(id);
        return success
            ? NoContent()
            : NotFound();
    }
}
