namespace PortFreight.MVC.Controllers;

public class PortsController(IPortFreightClient client) : Controller
{
    public async Task<IActionResult> Index()
    {
        var viewModel = await client.GetAsync<PortViewModel>("api/ports");
        return View(viewModel);
    }


    public async Task<IActionResult> Details(Guid id)
    {
        var viewModel = await client.GetAsync<PortViewModel>("api/ports", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpGet] public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreatePortViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        await client.CreateAsync("api/ports", viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var viewModel = await client.GetAsync<UpdatePortViewModel>("api/ports", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, UpdatePortViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index));

        await client.UpdateAsync("api/ports", id, viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var viewModel = await client.GetAsync<PortViewModel>("api/ports", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await client.DeleteAsync<PortViewModel>("api/ports", id);
        return RedirectToAction(nameof(Index));
    }
}

