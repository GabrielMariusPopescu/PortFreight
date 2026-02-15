namespace PortFreight.Api.Controllers;

/// <summary>
/// Provides API endpoints for managing tracking events associated with shipments.
/// </summary>
/// <remarks>This controller exposes endpoints to retrieve all tracking events for a specific shipment and to
/// create new tracking events. If no tracking events are found for a given shipment ID, the retrieval endpoint returns
/// a 404 Not Found response.</remarks>
/// <param name="service">The service used to perform operations related to tracking events, such as retrieving and creating tracking event
/// records.</param>
[ApiController]
[Route("api/[controller]")]
public class TrackingEventController(ITrackingEventService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all tracking events associated with the specified shipment identifier. If no tracking events are found for the given shipment ID, a 404 Not Found response is returned.
    /// </summary>
    /// <param name="shipmentId">The unique identifier of the shipment to track events for. Must be a valid <see cref="Guid"/>.</param>
    /// <returns>A <see cref="IActionResult"/> containing the tracking event details if found; otherwise, a NotFound result.</returns>
    [HttpGet("shipment/{shipmentId:Guid}")]
    public async Task<IActionResult> GetForShipment(Guid shipmentId)
    {
        var trackingEvents = (await service.GetTrackingEventsForShipmentAsync(shipmentId)).ToList();
        return !trackingEvents.Any() 
            ? NotFound() 
            : Ok(trackingEvents.Select(trackingEvent => trackingEvent.ToDto()));
    }

    /// <summary>
    /// Creates a new tracking event for the specified shipment identifier using the provided data transfer object (DTO). The DTO contains
    /// </summary>
    /// <param name="shipmentId">The unique identifier of the shipment to track events for. Must be a valid <see cref="Guid"/>.</param>
    /// <param name="dto">The data transfer object containing the details of the tracking event to create. Cannot be null.</param>
    /// <returns>A 201 Created response containing details of newly created tracking event, including its identifier.</returns>
    [HttpPost("{shipmentId:Guid}")]
    public async Task<IActionResult> Create(Guid shipmentId, CreateTrackingEventDto dto)
    {
        var trackingEvent = dto.ToTrackingEvent(shipmentId);
        var created = await service.AddTrackingEventAsync(trackingEvent);
        return Ok(created.ToDto());
    }
}