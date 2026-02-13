namespace PortFreight.Application.Services.Implementation;

public class VesselService : IVesselService
{
    private readonly IGenericRepository<Vessel> _repository;

    public VesselService(IGenericRepository<Vessel> repository)
    {
        _repository = repository;
    }

    public async Task<Vessel?> GetVesselAsync(Guid id) =>
        await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<Vessel>> GetAllVesselsAsync() =>
        await _repository.GetAllAsync();

    public async Task<Vessel> CreateVesselAsync(Vessel vessel)
    {
        await _repository.AddAsync(vessel);
        await _repository.SaveChangesAsync();
        return vessel;
    }

    public async Task<bool> UpdateVesselAsync(Vessel vessel)
    {
        var existing = await _repository.GetByIdAsync(vessel.Id);
        if (existing == null)
            return false;

        _repository.Update(vessel);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteVesselAsync(Guid id)
    {
        var vessel = await _repository.GetByIdAsync(id);
        if (vessel == null)
            return false;

        _repository.Delete(vessel);
        await _repository.SaveChangesAsync();
        return true;
    }
}
