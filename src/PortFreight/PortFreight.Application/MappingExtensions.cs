namespace PortFreight.Application;

public static class MappingExtensions
{
    public static Guid ToGuid(this string id)
    {
        var isValidGuid = Guid.TryParse(id, out Guid guid);
        return isValidGuid ? guid : Guid.Empty;
    }
}
