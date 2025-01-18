using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using gestioncommande.Models;
using gestioncommande.Enum;
namespace gestioncommande.Controllers;

using gestioncommande.Services.Impl;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
// [Authorize(Roles = "CLIENT")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserService _userService;

    private readonly ClientService _clientService;


    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserService userService, ClientService clientService)
    {
        _logger = logger;
        _context = context;
        _userService = userService;
        _clientService = clientService;
    }

    public async Task<IActionResult> Index()
    {



        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Home");
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
