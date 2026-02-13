namespace PortFreight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(ICustomerService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await service.GetAllCustomersAsync();
        return !customers.Any() ? NotFound() : Ok(customers.Select(customer => customer.ToDto()));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var customer = await service.GetCustomerAsync(id);
        return customer is null ? NotFound() : Ok(customer.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerDto dto)
    {
        var customer = dto.ToCustomer();
        var created = await service.CreateCustomerAsync(customer);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToDto());
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateCustomerDto dto)
    {
        if (id != dto.Id) 
            return BadRequest("ID mismatch");

        var existing = await service.GetCustomerAsync(id);
        if (existing is null) 
            return NotFound();

        dto.MapToEntity(existing);
        await service.UpdateCustomerAsync(existing);

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await service.DeleteCustomerAsync(id);
        return success ? NoContent() : NotFound();
    }
}
