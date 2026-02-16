namespace PortFreight.MVC.Controllers;

public class ContainersController(IPortFreightClient client) : Controller
{
    public async Task<IActionResult> Index()
    {
        var viewModel = await client.GetAsync<ContainerViewModel>("api/containers");
        return View(viewModel);
    }


    public async Task<IActionResult> Details(Guid id)
    {
        var viewModel = await client.GetAsync<ContainerViewModel>("api/containers", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpGet] public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateContainerViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        await client.CreateAsync("api/containers", viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var viewModel = await client.GetAsync<UpdateContainerViewModel>("api/containers", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, UpdateContainerViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index));

        await client.UpdateAsync("api/containers", id, viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var viewModel = await client.GetAsync<ContainerViewModel>("api/containers", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await client.DeleteAsync<ContainerViewModel>("api/containers", id);
        return RedirectToAction(nameof(Index));
    }
}