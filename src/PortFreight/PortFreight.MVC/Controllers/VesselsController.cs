namespace PortFreight.MVC.Controllers;

public class VesselsController(IPortFreightClient client) : Controller
{
    public async Task<IActionResult> Index()
    {
        var vessels = await client.GetAsync<VesselViewModel>("api/vessels");
        return View(vessels);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var vessel = await client.GetAsync<VesselViewModel>("api/vessels",id);
        return vessel == null ? NotFound() : View(vessel);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateVesselViewModel viewModel)
    {
        if (!ModelState.IsValid) 
            return View(viewModel);
        await client.CreateAsync("api/vessels", viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var vessel = await client.GetAsync<UpdateVesselViewModel>("api/vessels",id);
        return vessel == null ? NotFound() : View(vessel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, UpdateVesselViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index));

        await client.UpdateAsync("api/vessels", id, viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var viewModel = await client.GetAsync<VesselViewModel>("api/vessels",id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await client.DeleteAsync<VesselViewModel>("api/vessels",id);
        return RedirectToAction(nameof(Index));
    }
}