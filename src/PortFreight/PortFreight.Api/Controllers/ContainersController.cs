namespace PortFreight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContainersController(IContainerService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var containers = await service.GetAllContainersAsync();
        return !containers.Any() ? NotFound() : Ok(containers.Select(container => container.ToDto()));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var container = await service.GetContainerAsync(id);
        return container is null ? NotFound() : Ok(container.ToDto());
    }

    [HttpPost("{shipmentId:Guid}")]
    public async Task<IActionResult> Create(Guid shipmentId, CreateContainerDto dto)
    {
        var container = dto.ToContainer(shipmentId);
        var created = await service.CreateContainerAsync(container);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToDto());
    }

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

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await service.DeleteContainerAsync(id);
        return success ? NoContent() : NotFound();
    }
}
