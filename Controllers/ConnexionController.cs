using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using gestioncommande.Models;
using Microsoft.EntityFrameworkCore;



public class ConnexionController : Controller
{

    private readonly ApplicationDbContext _context;

    public ConnexionController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;
        if (claimUser.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Connexion");
    }

    [HttpPost]
    public async Task<IActionResult> Login(User model)
    {

        var user = await _context.users.FirstOrDefaultAsync(u => u.Login == model.Login);
        // Console.WriteLine("--------------------------------");
        // Console.WriteLine(user?.Login);
        // Console.WriteLine(user?.Password);
        // Console.WriteLine(user.Role.ToString());
        // Console.WriteLine("--------------------------------");
        if (user != null && model.Password == user.Password)
        {

            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,

            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties);

            return RedirectToAction("Index", "Home");
        }
        ViewData["ValidateMessage"] = "user not found";
        return View();
    }

}
