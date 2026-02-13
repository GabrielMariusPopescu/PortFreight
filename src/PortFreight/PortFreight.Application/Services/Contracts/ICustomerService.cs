namespace PortFreight.Application.Services.Contracts;

public interface ICustomerService
{
    Task<Customer?> GetCustomerAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task<bool> UpdateCustomerAsync(Customer customer);
    Task<bool> DeleteCustomerAsync(Guid id);
}
