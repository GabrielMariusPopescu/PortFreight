namespace PortFreight.Api.Controllers;

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