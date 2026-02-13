namespace PortFreight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortsController(IPortService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ports = await service.GetAllPortsAsync();
        return !ports.Any() 
            ? NotFound() 
            : Ok(ports.Select(port => port.ToDto()));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var port = await service.GetPortAsync(id);
        return port is null 
            ? NotFound() 
            : Ok(port.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePortDto dto)
    {
        var entity = dto.ToPort();
        var created = await service.CreatePortAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToDto());
    }

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

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await service.DeletePortAsync(id);
        return success ? NoContent() : NotFound();
    }
}