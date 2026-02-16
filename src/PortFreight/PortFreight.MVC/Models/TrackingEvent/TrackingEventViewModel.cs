namespace PortFreight.MVC.Models.TrackingEvent;

public class TrackingEventViewModel
{
    [Display(Name = "Id")]
    public Guid Id{get; set;}

    [Display(Name = "Event Type")]
    public string EventType{get; set;}

    [Display(Name = "Timestamp")]
    public DateTime Timestamp{get; set;}

    [Display(Name = "Location")]
    public string Location{get; set;}

    [Display(Name = "Shipment Id")]
    public Guid ShipmentId { get; set; }
}