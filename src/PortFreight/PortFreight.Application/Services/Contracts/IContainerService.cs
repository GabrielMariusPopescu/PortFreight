namespace PortFreight.Application.Services.Contracts;

public interface IContainerService
{
    Task<Container?> GetContainerAsync(Guid id);
    Task<IEnumerable<Container>> GetAllContainersAsync();
    Task<Container> CreateContainerAsync(Container container);
    Task<bool> UpdateContainerAsync(Container container);
    Task<bool> DeleteContainerAsync(Guid id);
}