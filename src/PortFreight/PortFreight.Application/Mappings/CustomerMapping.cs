namespace PortFreight.Application.Mappings;

public static class CustomerMapping
{
    public static CustomerDto ToDto(this Customer customer) =>
        new(customer.Id, customer.Name, customer.Email, customer.Phone);

    public static Customer ToCustomer(this CreateCustomerDto dto) =>
        new()
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone
        };

    public static void MapToEntity(this UpdateCustomerDto dto, Customer customer)
    {
        customer.Name = dto.Name;
        customer.Email = dto.Email;
        customer.Phone = dto.Phone;
    }
}
