namespace PortFreight.MVC.Controllers;

public class ShipmentsController(IPortFreightClient client) : Controller
{
    public async Task<IActionResult> Index()
    {
        var items = await client.GetAsync<ShipmentViewModel>("api/shipments");
        return View(items);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var item = await client.GetAsync<ShipmentViewModel>("api/shipments",id);
        return item == null ? NotFound() : View(item);
    }

    [HttpGet] public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateShipmentViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);
        await client.CreateAsync("api/shipments",viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var item = await client.GetAsync<ShipmentViewModel>("api/shipments", id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Guid id, UpdateShipmentViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index));
        
        await client.UpdateAsync("api/shipments",id, viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await client.DeleteAsync<ShipmentViewModel>("api/shipments",id);
        return RedirectToAction(nameof(Index));
    }
}