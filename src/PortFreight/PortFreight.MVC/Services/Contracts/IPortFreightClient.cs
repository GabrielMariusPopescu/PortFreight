namespace PortFreight.MVC.Services.Contracts;

public interface IPortFreightClient
{
    Task<IEnumerable<ShipmentViewModel>> GetShipmentsAsync();
    Task<ShipmentViewModel?> GetShipmentAsync(Guid id);
    Task<ShipmentViewModel> CreateShipmentAsync(CreateShipmentViewModel viewModel);
    Task UpdateShipmentAsync(Guid id, UpdateShipmentViewModel viewModel);
    Task DeleteShipmentAsync(Guid id);
}