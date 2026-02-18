namespace PortFreight.Application.Tests.Customers;

[ExcludeFromCodeCoverage]
public class RandomisedCustomerBuilder(Guid? id = null, int? seed = null)
{
    private readonly Random _random = seed.HasValue 
        ? new Random(seed.Value) 
        : new Random();

    private Guid? _id = id ?? RandomGuid();
    private string? _name;
    private string? _email;
    private string? _phone;
    private ICollection<Shipment>? _shipments;

    public RandomisedCustomerBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public RandomisedCustomerBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public RandomisedCustomerBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public RandomisedCustomerBuilder WithPhone(string phone)
    {
        _phone = phone;
        return this;
    }

    public RandomisedCustomerBuilder WithShipments(ICollection<Shipment> shipments)
    {
        _shipments = shipments;
        return this;
    }

    public Customer Build() =>
        new()
        {
            Id = _id ?? RandomGuid(),
            Name = _name ?? RandomName(),
            Email = _email ?? RandomEmail(),
            Phone = _phone ?? RandomPhone(),
            Shipments = _shipments ?? []
        };

    public static implicit operator Customer(RandomisedCustomerBuilder builder)
        => builder.Build();

    private string RandomName()
    {
        var first = RandomString(5, 10);
        var last = RandomString(5, 12);
        return $"{first} {last}";
    }

    private string RandomEmail()
    {
        var user = RandomString(5, 12).ToLowerInvariant();
        var domain = RandomString(5, 10).ToLowerInvariant();
        return $"{user}@{domain}.com";
    }

    private string RandomPhone() => 
        $"{_random.Next(100, 999)}-" +
        $"{_random.Next(100, 999)}-" +
        $"{_random.Next(1000, 9999)}";

    private string RandomString(int minLength, int maxLength)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int length = _random.Next(minLength, maxLength + 1);

        return new string(Enumerable.Range(0, length)
            .Select(_ => chars[_random.Next(chars.Length)])
            .ToArray());
    }

    private static Guid RandomGuid()
    {
        var bytes = new byte[16];
        RandomNumberGenerator.Fill(bytes);
        return new Guid(bytes);
    }
}