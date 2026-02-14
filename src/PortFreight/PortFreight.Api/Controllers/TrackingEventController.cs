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
    [HttpGet("shipment/{shipmentId:Guid}")]
    public async Task<IActionResult> GetForShipment(Guid shipmentId)
    {
        var trackingEvents = await service.GetTrackingEventsForShipmentAsync(shipmentId);
        return !trackingEvents.Any() 
            ? NotFound() 
            : Ok(trackingEvents.Select(trackingEvent => trackingEvent.ToDto()));
    }

    [HttpPost("{shipmentId:Guid}")]
    public async Task<IActionResult> Create(Guid shipmentId, CreateTrackingEventDto dto)
    {
        var trackingEvent = dto.ToTrackingEvent(shipmentId);
        var created = await service.AddTrackingEventAsync(trackingEvent);
        return Ok(created.ToDto());
    }
}