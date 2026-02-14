namespace PortFreight.Api.Controllers;

/// <summary>
/// Handles HTTP requests for customer-related operations, including retrieving, creating, updating, and deleting
/// customers.
/// </summary>
/// <remarks>This controller provides endpoints for managing customers in the system. It supports standard RESTful
/// operations and ensures validation of input data for create and update operations.</remarks>
/// <param name="service">The service used to perform operations on customer data, such as retrieving and modifying customer records.</param>
[ApiController]
[Route("api/[controller]")]
public class CustomersController(ICustomerService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all customers and returns them as a collection of data transfer objects (DTOs).
    /// </summary>
    /// <remarks>This method asynchronously fetches all customers from the underlying service and transforms
    /// each customer entity into a DTO for the response. If no customers exist, the response will indicate that no
    /// resources were found.</remarks>
    /// <returns>An <see cref="IActionResult"/> that contains a list of customer DTOs with a 200 OK response if any customers are
    /// found; otherwise, a 404 Not Found response.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await service.GetAllCustomersAsync();
        return !customers.Any() ? NotFound() : Ok(customers.Select(customer => customer.ToDto()));
    }

    /// <summary>
    /// Retrieves the customer details associated with the specified unique identifier.
    /// </summary>
    /// <remarks>This method asynchronously fetches the customer data from the service. If the customer does
    /// not exist, a 404 Not Found response is returned.</remarks>
    /// <param name="id">The unique identifier of the customer to retrieve. Must be a valid GUID.</param>
    /// <returns>An IActionResult containing the customer details in DTO format if found; otherwise, returns a NotFound result.</returns>
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var customer = await service.GetCustomerAsync(id);
        return customer is null ? NotFound() : Ok(customer.ToDto());
    }

    /// <summary>
    /// Creates a new customer using the specified data transfer object.
    /// </summary>
    /// <remarks>The method validates the input data before creating the customer. If validation fails, the
    /// response includes details about the validation errors in the HTTP context.</remarks>
    /// <param name="dto">The data transfer object containing the information required to create a new customer. Must satisfy all
    /// validation requirements.</param>
    /// <returns>A 201 Created response containing the newly created customer's data if the operation succeeds; otherwise, a 400
    /// Bad Request response with validation errors.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerDto dto)
    {
        var validator = new CreateCustomerValidator();
        var validationResult = validator.Validate(dto);
        if (!validationResult.IsValid) 
        {
            HttpContext.Items["ValidationErrors"] = validationResult.Errors;
            return BadRequest();
        }
        var customer = dto.ToCustomer();
        var created = await service.CreateCustomerAsync(customer);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created.ToDto());
    }

    /// <summary>
    /// Updates the details of an existing customer with the specified identifier.
    /// </summary>
    /// <remarks>The method validates that the provided identifier matches the identifier in the update data
    /// and that the customer exists before performing the update.</remarks>
    /// <param name="id">The unique identifier of the customer to update. Must match the identifier in <paramref name="dto"/>.</param>
    /// <param name="dto">An object containing the updated customer information. The <c>Id</c> property must match the <paramref
    /// name="id"/> parameter.</param>
    /// <returns>An <see cref="IActionResult"/> that indicates the result of the operation. Returns <see cref="NoContentResult"/>
    /// if the update is successful; otherwise, returns <see cref="BadRequestResult"/> if the identifiers do not match,
    /// or <see cref="NotFoundResult"/> if the customer does not exist.</returns>
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

    /// <summary>
    /// Deletes the customer with the specified unique identifier.
    /// </summary>
    /// <remarks>This operation is asynchronous. Ensure that the specified identifier corresponds to an
    /// existing customer before calling this method.</remarks>
    /// <param name="id">The unique identifier of the customer to delete. Must be a valid <see cref="System.Guid"/>.</param>
    /// <returns>An <see cref="IActionResult"/> that indicates the result of the operation. Returns <see cref="NoContentResult"/>
    /// if the customer was deleted successfully; otherwise, <see cref="NotFoundResult"/> if the customer does not
    /// exist.</returns>
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await service.DeleteCustomerAsync(id);
        return success ? NoContent() : NotFound();
    }
}
