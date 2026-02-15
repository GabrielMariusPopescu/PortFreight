namespace PortFreight.MVC.Controllers;

public class ShipmentsController(IPortFreightClient client) : Controller
{
    public async Task<IActionResult> Index()
    {
        var items = await client.GetShipmentsAsync();
        return View(items);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var item = await client.GetShipmentAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpGet] public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateShipmentViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);
        await client.CreateShipmentAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var item = await client.GetShipmentAsync(id);
        return item == null ? NotFound() : View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Guid id, UpdateShipmentViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index));
        
        await client.UpdateShipmentAsync(id, viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await client.DeleteShipmentAsync(id);
        return RedirectToAction(nameof(Index));
    }
}