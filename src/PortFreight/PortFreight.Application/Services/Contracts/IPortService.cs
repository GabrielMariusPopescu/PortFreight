namespace PortFreight.Application.Services.Contracts;

public interface IPortService
{
    Task<Port?> GetPortAsync(Guid id);
    Task<IEnumerable<Port>> GetAllPortsAsync();
    Task<Port> CreatePortAsync(Port port);
    Task<bool> UpdatePortAsync(Port port);
    Task<bool> DeletePortAsync(Guid id);
}
