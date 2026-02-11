using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PortFreight.Web.Utilities;

public class BaseMsd1PageModel : PageModel
{
    protected readonly string MSD1Key = "MSD1Key";
    protected readonly string MSD1Success = "MSD1Success";
    protected readonly string MSD3Key = "MSD3Key";
    protected readonly string MSD23Key = "MSD23Key";
    protected readonly string RespondentVMKey = "RespondentVMKey";
    private ViewData _viewBag;

    public dynamic ViewBag
    {
        get
        {
                if (_viewBag == null)
                {
                    _viewBag = new ViewData(() => ViewData);
                }
                return _viewBag;
            }
    }
}

public class ViewData
{
    public ViewData(Func<ViewDataDictionary> func)
    {
        throw new NotImplementedException();
    }
}