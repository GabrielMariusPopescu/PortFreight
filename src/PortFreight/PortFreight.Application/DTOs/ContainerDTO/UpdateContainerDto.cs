namespace PortFreight.Application.DTOs.ContainerDTO;

public record UpdateContainerDto(
    Guid Id,
    string ContainerNumber,
    string Type,
    decimal Weight
);