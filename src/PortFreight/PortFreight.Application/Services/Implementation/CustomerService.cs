namespace PortFreight.Application.Services.Implementation;

public class CustomerService : ICustomerService
{
    private readonly IGenericRepository<Customer> _repository;

    public CustomerService(IGenericRepository<Customer> repository)
    {
        _repository = repository;
    }

    public async Task<Customer?> GetCustomerAsync(Guid id) =>
        await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync() =>
        await _repository.GetAllAsync();

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        await _repository.AddAsync(customer);
        await _repository.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {
        var existing = await _repository.GetByIdAsync(customer.Id);
        if (existing == null)
            return false;

        _repository.Update(customer);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCustomerAsync(Guid id)
    {
        var customer = await _repository.GetByIdAsync(id);
        if (customer == null)
            return false;

        _repository.Delete(customer);
        await _repository.SaveChangesAsync();
        return true;
    }
}