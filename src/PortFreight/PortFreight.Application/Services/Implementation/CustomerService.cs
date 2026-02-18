namespace PortFreight.Application.Services.Implementation;

public class CustomerService(IGenericRepository<Customer> repository) 
    : ICustomerService
{
    public async Task<Customer?> GetCustomerAsync(Guid id) =>
        await repository.GetByIdAsync(id);

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync() =>
        await repository.GetAllAsync();

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        await repository.AddAsync(customer);
        await repository.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        var existing = await repository.GetByIdAsync(customer.Id);
        if (existing == null)
            return false;

        var updated = repository.Update(customer);
        await repository.SaveChangesAsync();
        return updated;
    }

    public async Task<bool> DeleteCustomerAsync(Guid id)
    {
        var customer = await repository.GetByIdAsync(id);
        if (customer == null)
            return false;

        var deleted = repository.Delete(customer);
        await repository.SaveChangesAsync();
        return deleted;
    }
}