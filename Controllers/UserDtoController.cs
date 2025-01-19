
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestioncommande.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using gestioncommande.Enum;

namespace gestioncommande.Controllers
{
    public class UserDtoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserDtoController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDto user1)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.CreateAt = DateTime.UtcNow;
                user.UpdateAt = DateTime.UtcNow;
                user.Login = user1.Login;
                user.Password = user1.Password;
                user.Nom = user1.Nom;
                user.Prenom = user1.Prenom;
                user.telephone = user1.telephone;
                user.Role = Role.CLIENT;
                Client client = new Client();
                client.addresse = user1.Adresse;
                client.Solde = 0;
                client.users = user;
                _context.Add(client);
                _context.Add(user);
                await _context.SaveChangesAsync();
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

                Console.WriteLine("tu essssssssssssssssssssssss reeeeeeeeeeeeeeeeeeeennnnnnnnnnnteee LA AU DESSSSSSSSUUUUUUUUUUUUUUU");

                return RedirectToAction("Index", "Home");

            }
            Console.WriteLine("tu essssssssssssssssssssssss reeeeeeeeeeeeeeeeeeeennnnnnnnnnnteee");
            return View(user1);
        }
    }
}