namespace PortFreight.Domain.Enums;

public enum ShipmentStatus
{
    [Display(Name = "Booked")]
    Booked,

    [Display(Name = "In Transit")]
    InTransit,

    [Display(Name = "Arrived")]
    Arrived,

    [Display(Name = "Customs Cleared")]
    CustomsCleared,

    [Display(Name = "Released")]
    Released
}
