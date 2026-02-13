using PortFreight.Domain.Entities;
namespace PortFreight.Application.Services.Contracts;

public interface IShipmentService
{
    Task<Shipment?> GetShipmentAsync(Guid id);
    Task<IEnumerable<Shipment>> GetAllShipmentsAsync();
    Task<Shipment> CreateShipmentAsync(Shipment shipment);
    Task<bool> UpdateShipmentStatusAsync(Guid shipmentId, ShipmentStatus newStatus);
    Task<bool> AddContainerToShipmentAsync(Guid shipmentId, Container container);
    Task<bool> AddTrackingEventAsync(Guid shipmentId, TrackingEvent trackingEvent);
}

