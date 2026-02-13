namespace PortFreight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipmentsController(IShipmentService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var shipments = await service.GetAllShipmentsAsync();
        return !shipments.Any() ? NotFound() : Ok(shipments.Select(shipment => shipment.ToDto()));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var shipment = await service.GetShipmentAsync(id);
        return shipment is null ? NotFound() : Ok(shipment.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateShipmentDto dto)
    {
        var shipment = dto.ToShipment();
        var created = await service.CreateShipmentAsync(shipment);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToDto());
    }

    [HttpPatch("{id:Guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, UpdateShipmentStatusDto dto)
    {
        var success = await service.UpdateShipmentStatusAsync(id, dto.Status);
        return success ? NoContent() : NotFound();
    }
}