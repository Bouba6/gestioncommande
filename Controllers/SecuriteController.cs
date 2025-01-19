using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestioncommande.Models;
using gestioncommande.Services.Impl;

namespace gestioncommande.Controllers
{
    public class SecuriteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecuriteController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Error404()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Connexion");
            }
            return View();
        }
    }
}