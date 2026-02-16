namespace PortFreight.MVC.Controllers;

public class TrackingEventsController(IPortFreightClient client) : Controller
{
    public async Task<IActionResult> Index(Guid shipmentId)
    {
        var viewModel = await client.GetAsync<TrackingEventViewModel>($"api/trackingEvents/shipments/{shipmentId}");
        return View(viewModel);
    }

    public async Task<IActionResult> Details(Guid shipmentId)
    {
        var viewModel = await client.GetAsync<TrackingEventViewModel>("api/trackingEvents", shipmentId);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTrackingEventViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        await client.CreateAsync("api/trackingEvents", viewModel);
        return RedirectToAction(nameof(Index));
    }
}
