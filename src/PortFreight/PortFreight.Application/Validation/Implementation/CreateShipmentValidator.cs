namespace PortFreight.Application.Validation.Implementation;

public class CreateShipmentValidator : BaseValidator<CreateShipmentDto>
{
    public override void ValidateRules(CreateShipmentDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.ReferenceNumber))
            AddError(nameof(dto.ReferenceNumber), "Reference number is required.");

        if (dto.CustomerId == Guid.Empty)
            AddError(nameof(dto.CustomerId), "CustomerId must be valid.");

        if (dto.OriginPortId == Guid.Empty)
            AddError(nameof(dto.OriginPortId), "OriginPortId must be valid.");

        if (dto.DestinationPortId == Guid.Empty)
            AddError(nameof(dto.DestinationPortId), "DestinationPortId must be valid.");

        if (dto.VesselVoyageId == Guid.Empty)
            AddError(nameof(dto.VesselVoyageId), "VesselVoyageId must be valid.");
    }
}