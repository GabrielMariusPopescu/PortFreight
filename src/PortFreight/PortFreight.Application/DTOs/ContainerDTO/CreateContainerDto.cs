namespace PortFreight.Application.DTOs.ContainerDTO;

public record CreateContainerDto(
    string ContainerNumber,
    string Type,
    decimal Weight
);
