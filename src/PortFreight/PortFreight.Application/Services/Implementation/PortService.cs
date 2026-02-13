namespace PortFreight.Application.Services.Implementation;

public class PortService : IPortService
{
    private readonly IGenericRepository<Port> _repository;

    public PortService(IGenericRepository<Port> repository)
    {
        _repository = repository;
    }

    public async Task<Port?> GetPortAsync(Guid id) =>
        await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<Port>> GetAllPortsAsync() =>
        await _repository.GetAllAsync();

    public async Task<Port> CreatePortAsync(Port port)
    {
        await _repository.AddAsync(port);
        await _repository.SaveChangesAsync();
        return port;
    }

    public async Task<bool> UpdatePortAsync(Port port)
    {
        var existing = await _repository.GetByIdAsync(port.Id);
        if (existing == null)
            return false;

        _repository.Update(port);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletePortAsync(Guid id)
    {
        var port = await _repository.GetByIdAsync(id);
        if (port == null)
            return false;

        _repository.Delete(port);
        await _repository.SaveChangesAsync();
        return true;
    }
}
