namespace PortFreight.Application.Mappings;

public static class PortMapping
{
    public static PortDto ToDto(this Port port) =>
        new(port.Id, port.Name, port.UNLocode, port.Country);

    public static Port ToEntity(this CreatePortDto dto) =>
        new()
        {
            Name = dto.Name,
            UNLocode = dto.UNLocode,
            Country = dto.Country
        };

    public static void MapToEntity(this UpdatePortDto dto, Port port)
    {
        port.Name = dto.Name;
        port.UNLocode = dto.UNLocode;
        port.Country = dto.Country;
    }
}
