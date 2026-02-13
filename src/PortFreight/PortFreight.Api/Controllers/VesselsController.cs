namespace PortFreight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VesselsController(IVesselService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vessels = await service.GetAllVesselsAsync();
        return !vessels.Any() ? NotFound() : Ok(vessels.Select(vessel => vessel.ToDto()));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var vessel = await service.GetVesselAsync(id);
        return vessel is null ? NotFound() : Ok(vessel.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVesselDto dto)
    {
        var entity = dto.ToVessel();
        var created = await service.CreateVesselAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToDto());
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateVesselDto dto)
    {
        if (id != dto.Id) 
            return BadRequest("ID mismatch");

        var existing = await service.GetVesselAsync(id);
        if (existing is null) return NotFound();

        dto.MapToEntity(existing);
        await service.UpdateVesselAsync(existing);

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await service.DeleteVesselAsync(id);
        return success ? NoContent() : NotFound();
    }
}