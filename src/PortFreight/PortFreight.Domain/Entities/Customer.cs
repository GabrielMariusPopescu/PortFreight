namespace PortFreight.Domain.Entities;

/// <summary>
/// Customer entity represents a customer in the port freight system.
/// It contains essential information about the customer,
/// including their name, contact details, and associated shipments.
/// </summary>
public class Customer
{
    /// <summary>
    /// Unique identifier for the customer, generated as an
    /// unique identifier to ensure global uniqueness across the system.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Name of the customer, which is a required field.
    /// This property is essential for identifying the customer and
    /// is used in various operations such as displaying customer information
    /// and associating shipments with the correct customer.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Customer email address, which is a required field.
    /// This property is essential for communication purposes,
    /// allowing the system to send notifications.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Phone number of the customer, which is a required field.
    /// This property is essential for communication purposes,
    /// allowing the system to contact the customer.
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Collection of shipments associated with the customer.
    /// This property represents the relationship between the customer and their shipments.
    /// </summary>
    public ICollection<Shipment> Shipments { get; init; } = [];
}
