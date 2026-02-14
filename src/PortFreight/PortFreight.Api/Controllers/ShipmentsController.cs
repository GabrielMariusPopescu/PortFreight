namespace PortFreight.Api.Controllers;

/// <summary>
/// Provides HTTP endpoints for managing shipment records, including retrieving, creating, and updating shipment
/// statuses.
/// </summary>
/// <remarks>This controller exposes endpoints for common shipment operations in the system. It supports
/// retrieving all shipments, fetching a specific shipment by its unique identifier, creating new shipments, and
/// updating the status of existing shipments. Each endpoint returns appropriate HTTP responses based on the outcome of
/// the operation.</remarks>
/// <param name="service">The service used to perform shipment-related operations, such as retrieving, creating, and updating shipment
/// records.</param>
[ApiController]
[Route("api/[controller]")]
public class ShipmentsController(IShipmentService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all shipments as data transfer objects (DTOs).
    /// </summary>
    /// <remarks>This method asynchronously fetches all available shipments and returns them as DTOs. If no
    /// shipments are found, the response is a 404 Not Found. Use this endpoint to obtain a complete list of shipments
    /// for further processing or display.</remarks>
    /// <returns>An <see cref="IActionResult"/> containing a collection of shipment DTOs if any exist; otherwise, a 404 Not Found
    /// result.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var shipments = await service.GetAllShipmentsAsync();
        return !shipments.Any() ? NotFound() : Ok(shipments.Select(shipment => shipment.ToDto()));
    }

    /// <summary>
    /// Retrieves the shipment details for the specified unique identifier.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to fetch the shipment. If no shipment exists
    /// for the specified identifier, the response is a 404 Not Found.</remarks>
    /// <param name="id">The unique identifier of the shipment to retrieve. Must be a valid <see cref="Guid"/>.</param>
    /// <returns>An <see cref="IActionResult"/> containing the shipment details if found; otherwise, a NotFound result.</returns>
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var shipment = await service.GetShipmentAsync(id);
        return shipment is null ? NotFound() : Ok(shipment.ToDto());
    }

    /// <summary>
    /// Creates a new shipment using the specified shipment data.
    /// </summary>
    /// <remarks>The created shipment can be retrieved using the identifier returned in the response. This
    /// method is asynchronous and should be awaited.</remarks>
    /// <param name="dto">The data transfer object containing the details of the shipment to create. Cannot be null.</param>
    /// <returns>A 201 Created response containing the details of the newly created shipment, including its identifier.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateShipmentDto dto)
    {
        var shipment = dto.ToShipment();
        var created = await service.CreateShipmentAsync(shipment);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToDto());
    }

    /// <summary>
    /// Updates the status of an existing shipment identified by the specified unique identifier.
    /// </summary>
    /// <remarks>This operation is asynchronous. Ensure that the provided status value in <paramref
    /// name="dto"/> is valid and supported by the system.</remarks>
    /// <param name="id">The unique identifier of the shipment whose status is to be updated.</param>
    /// <param name="dto">An object containing the new status information to apply to the shipment.</param>
    /// <returns>An <see cref="IActionResult"/> that indicates the result of the operation. Returns <see langword="NoContent"/>
    /// if the update is successful; otherwise, <see langword="NotFound"/> if the shipment does not exist.</returns>
    [HttpPatch("{id:Guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateShipmentStatusDto dto)
    {
        var success = await service.UpdateShipmentStatusAsync(id, dto.Status);
        return success ? NoContent() : NotFound();
    }
}