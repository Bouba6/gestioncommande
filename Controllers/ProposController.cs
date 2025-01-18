using Microsoft.AspNetCore.Mvc;
namespace gestioncommande.Controllers;


public class ProposController : Controller
{


    public ProposController()
    {
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
}