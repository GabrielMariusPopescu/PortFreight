namespace PortFreight.Application;

public static class MappingExtensions
{
    public static IEnumerable<TOut> MapList<TIn, TOut>(this IEnumerable<TIn> list, Func<TIn, TOut> map)
        => list.Select(map);
}
