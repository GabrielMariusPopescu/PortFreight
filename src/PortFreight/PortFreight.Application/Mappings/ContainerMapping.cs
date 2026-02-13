namespace PortFreight.Application.Mappings;

public static class ContainerMapping
{
    public static ContainerDto ToDto(this Container container) =>
        new(container.Id, container.ContainerNumber, container.Type, container.Weight, container.ShipmentId);

    public static Container ToContainer(this CreateContainerDto dto, Guid shipmentId) =>
        new()
        {
            ContainerNumber = dto.ContainerNumber,
            Type = dto.Type,
            Weight = dto.Weight,
            ShipmentId = shipmentId
        };

    public static void MapToEntity(this UpdateContainerDto dto, Container container)
    {
        container.ContainerNumber = dto.ContainerNumber;
        container.Type = dto.Type;
        container.Weight = dto.Weight;
    }
}
