using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestioncommande.Models;
using System.Reflection;
using gestioncommande.Helpers;
using Microsoft.AspNetCore.Authorization;
namespace gestioncommande.Controllers
{

    public class ProduitController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProduitService _produitService;

        public ProduitController(ApplicationDbContext context, ProduitService produitService)
        {

            _context = context;
            _produitService = produitService;

        }

        // GET: Produit
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
        {

            var products = from d in _context.produit
                           select d;
            //pagination

            int totalProducts = await products.CountAsync();

            if (pageSize > 0)
            {
                products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

            int totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;




            return View(await products.ToListAsync());
        }


        [HttpPost]

        public async Task<IActionResult> ProduitDetails([FromBody] Produit model)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Connexion");
            }

            Produit produit = await _produitService.GetById(model.Id);

            return Json(new { success = true, produit = produit });
        }

        public async Task<IActionResult> GetProduit(int id)
        {
            Produit produit = await _produitService.GetById(id);
            return View(produit);

        }




        [Authorize(Roles = "RS")]
        // GET: Produit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Connexion");
            }
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.produit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }



        [Authorize(Roles = "CLIENT")]
        // GET: Produit/Create
        public IActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "RS")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produit produit)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Connexion");
            }
            ModelState.Remove("detailCommandes");
            if (produit.image != null)
            {
                // Nouveau chemin pour l'image
                var path = Path.Combine("wwwroot/image/produit", produit.image.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await produit.image.CopyToAsync(stream);
                }

                // Mise à jour du chemin de l'image
                produit.FileName = $"/image/produit/{produit.image.FileName}";
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(produit);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Gestion des exceptions en cas d'erreur
                    ModelState.AddModelError(string.Empty, "Une erreur s'est produite lors de l'ajout du produit.");
                }
            }

            return View(produit);
        }



        // POST: Produit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "RS")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Produit produit)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Connexion");
            }
            ModelState.Remove("detailCommandes");
            if (produit.image != null)
            {
                // Nouveau chemin pour l'image
                var path = Path.Combine("wwwroot/image/produit", produit.image.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    produit.image.CopyTo(stream);
                }

                // Mise à jour du chemin de l'image
                produit.FileName = $"/image/produit/{produit.image.FileName}";
            }
            else
            {
                // Si aucune nouvelle image n'est ajoutée, garder l'image existante
                ModelState.Remove("FileName");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(produit);
        }



        // [HttpPost]
        // public IActionResult AddToCart(int productId, int quantity)
        // {

        //     var produit = _context.produit.FirstOrDefault(a => a.Id == productId);
        //     var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier") ?? new Panier();
        //     panier.AjouterProduits(produit, quantity);
        //     HttpContext.Session.SetObjectAsJson("panier", panier);


        //     return RedirectToAction("Index", "Produit");
        // }


        [HttpPost]
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> AddToCart([FromBody] Produit model)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Connexion");
            }

            Produit produit = await _produitService.GetById(model.Id);
            var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier") ?? new Panier();
            panier.AjouterProduits(produit, 1);
            HttpContext.Session.SetObjectAsJson("panier", panier);
            // compter le nbre de detailCommande
            var count = panier.detailCommande.Count();

            return Json(new { success = true, counting = count });
        }


        [Authorize(Roles = "RS")]        // GET: Produit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.produit.FindAsync(id);
            if (produit == null)
            {
                return NotFound();
            }
            return View(produit);
        }

        // POST: Produit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // [Authorize(Roles = "RS")]
        // public async Task<IActionResult> Edit(int id, Produit produit)
        // {
        //     if (id != produit.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             // Console.WriteLine("ç_èjhgfdghxj,;wxckjhgvfcvbn,;,nbvcxn,;");
        //             // Console.WriteLine(produit.FileName);
        //             // Console.WriteLine(produit.image.FileName);
        //             // Console.WriteLine("ç_èjhgfdghxj,;wxckjhgvfcvbn,;,nbvcxn,;");
        //             // var image = $"/image/produit/{produit.image.FileName}";
        //             // produit.FileName = image;
        //             _context.Update(produit);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!ProduitExists(produit.Id))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(produit);
        // }

        // GET: Produit/Delete/5
        [Authorize(Roles = "RS")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Connexion");
            }
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.produit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // POST: Produit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RS")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Connexion");
            }
            var produit = await _context.produit.FindAsync(id);
            if (produit != null)
            {
                _context.produit.Remove(produit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduitExists(int id)
        {
            return _context.produit.Any(e => e.Id == id);
        }
    }
}
