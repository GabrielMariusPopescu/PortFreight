namespace PortFreight.Application.Mappings;

public static class VesselMapping
{
    public static VesselDto ToDto(this Vessel vessel) =>
        new(vessel.Id, vessel.Name, vessel.IMO, vessel.CapacityTEU);

    public static Vessel ToVessel(this CreateVesselDto dto) =>
        new()
        {
            Name = dto.Name,
            IMO = dto.IMO,
            CapacityTEU = dto.CapacityTEU
        };

    public static void MapToEntity(this UpdateVesselDto dto, Vessel vessel)
    {
        vessel.Name = dto.Name;
        vessel.IMO = dto.IMO;
        vessel.CapacityTEU = dto.CapacityTEU;
    }
}
