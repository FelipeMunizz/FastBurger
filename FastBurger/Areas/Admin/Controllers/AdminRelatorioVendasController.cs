using FastBurger.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastBurger.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminRelatorioVendasController : Controller
{
    private readonly RelatorioVendaService _relatorioVendaService;

    public AdminRelatorioVendasController(RelatorioVendaService relatorioVendaService)
    {
        _relatorioVendaService = relatorioVendaService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> RelatorioVendasSimples(DateTime? startDate, DateTime? endDate)
    {
        if(!startDate.HasValue)
        {
            startDate = new DateTime(DateTime.Now.Year, 1, 1);
        }

        if(!endDate.HasValue)
        {
            endDate = DateTime.Now;
        }

        ViewData["startDate"] = startDate.Value.ToString("yyyy-MM-dd");
        ViewData["endDate"] = endDate.Value.ToString("yyyy-MM-dd");

        var result = await _relatorioVendaService.FindByDateAsync(startDate.Value, endDate.Value);
        return View(result);
    }
}
