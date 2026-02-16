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
    /// Retrieves all tracking events associated with a specific shipment.
    /// </summary>
    /// <param name="shipmentId">The unique identifier (GUID) of the shipment.</param>
    /// <returns>
    /// 200 OK with a collection of tracking events if any exist;  
    /// 404 Not Found if no tracking events are found for the shipment.
    /// </returns>
    [HttpGet("shipment/{shipmentId:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForShipment(Guid shipmentId)
    {
        var trackingEvents =
            (await service.GetTrackingEventsForShipmentAsync(shipmentId)).ToList();

        return !trackingEvents.Any()
            ? NotFound()
            : Ok(trackingEvents.Select(trackingEvent => trackingEvent.ToDto()));
    }

    /// <summary>
    /// Creates a new tracking event for a specific shipment.
    /// </summary>
    /// <param name="shipmentId">
    /// The unique identifier (GUID) of the shipment to which the tracking event belongs.
    /// </param>
    /// <param name="dto">The data transfer object containing tracking event details.</param>
    /// <returns>
    /// 200 OK with the created tracking event;  
    /// 400 Bad Request if the input is invalid.
    /// </returns>
    [HttpPost("{shipmentId:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Guid shipmentId, CreateTrackingEventDto dto)
    {
        var trackingEvent = dto.ToTrackingEvent(shipmentId);
        var created = await service.AddTrackingEventAsync(trackingEvent);

        return Ok(created.ToDto());
    }
}