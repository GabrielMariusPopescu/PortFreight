using System;
using System.Collections.Generic;
using System.Text;

namespace PortFreight.Application.Services.Implementation;

public class ContainerService : IContainerService
{
    private readonly IGenericRepository<Container> _repository;

    public ContainerService(IGenericRepository<Container> repository)
    {
        _repository = repository;
    }

    public async Task<Container?> GetContainerAsync(Guid id) =>
        await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<Container>> GetAllContainersAsync() =>
        await _repository.GetAllAsync();

    public async Task<Container> CreateContainerAsync(Container container)
    {
        await _repository.AddAsync(container);
        await _repository.SaveChangesAsync();
        return container;
    }

    public async Task<bool> UpdateContainerAsync(Container container)
    {
        var existing = await _repository.GetByIdAsync(container.Id);
        if (existing == null)
            return false;

        _repository.Update(container);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteContainerAsync(Guid id)
    {
        var container = await _repository.GetByIdAsync(id);
        if (container == null)
            return false;

        _repository.Delete(container);
        await _repository.SaveChangesAsync();
        return true;
    }
}