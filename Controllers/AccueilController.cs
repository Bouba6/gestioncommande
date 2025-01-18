using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestioncommande.Models;
using gestioncommande.Helpers;
using Microsoft.AspNetCore.Razor.Language;
using gestioncommande.Enum;

namespace gestioncommande.Controllers
{
    public class AccueilController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccueilController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}