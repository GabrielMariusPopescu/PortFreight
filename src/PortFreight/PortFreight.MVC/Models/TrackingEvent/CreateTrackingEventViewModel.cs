namespace PortFreight.MVC.Models.TrackingEvent;

public class CreateTrackingEventViewModel
{
    [Display(Name = "Event Type")]
    public string EventType { get; set; }

    [Display(Name = "Location")]
    public string Location { get; set; }

    [Display(Name = "Shipment Id")]
    public Guid ShipmentId { get; set; }
}