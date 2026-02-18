namespace PortFreight.MVC.Controllers;

public class UsersController(IPortFreightClient client) : Controller
{
    public async Task<IActionResult> Index()
    {
        var viewModel = await client.GetAsync<UserViewModel>("api/users");
        return View(viewModel);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var viewModel = await client.GetAsync<UserViewModel>("api/users", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        await client.CreateAsync("api/users", viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var viewModel = await client.GetAsync<UpdateUserViewModel>("api/users", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, UpdateUserViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index));

        await client.UpdateAsync("api/users", id, viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var viewModel = await client.GetAsync<UserViewModel>("api/users", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await client.DeleteAsync<UserViewModel>("api/users", id);
        return RedirectToAction(nameof(Index));
    }
}
