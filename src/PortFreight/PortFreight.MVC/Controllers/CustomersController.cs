namespace PortFreight.MVC.Controllers;

public class CustomersController(IPortFreightClient client) : Controller
{
    public async Task<IActionResult> Index()
    {
        var viewModel = await client.GetAsync<CustomerViewModel>("api/customers");
        return View(viewModel);
    }


    public async Task<IActionResult> Details(Guid id)
    {
        var viewModel = await client.GetAsync<CustomerViewModel>("api/customers", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpGet] public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        await client.CreateAsync("api/customers", viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var viewModel = await client.GetAsync<UpdateCustomerViewModel>("api/customers", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, UpdateCustomerViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index));

        await client.UpdateAsync("api/customers", id, viewModel);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var viewModel = await client.GetAsync<CustomerViewModel>("api/customers", id);
        return viewModel == null ? NotFound() : View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await client.DeleteAsync<CustomerViewModel>("api/customers", id);
        return RedirectToAction(nameof(Index));
}
}
