namespace PortFreight.Application.Services.Contracts;

public interface IVesselService
{
    Task<Vessel?> GetVesselAsync(Guid id);
    Task<IEnumerable<Vessel>> GetAllVesselsAsync();
    Task<Vessel> CreateVesselAsync(Vessel vessel);
    Task<bool> UpdateVesselAsync(Vessel vessel);
    Task<bool> DeleteVesselAsync(Guid id);
}

