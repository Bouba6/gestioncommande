using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestioncommande.Models;
using gestioncommande.Helpers;
using gestioncommande.Enum;
using gestioncommande.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using System.Data.Common;

namespace gestioncommande.Controllers
{
    // [Authorize(Roles = "RS")]
    public class CommandeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CommandeService _commandeService;
        private readonly UserService _userService;

        private readonly ClientService _clientService;


        public CommandeController(ApplicationDbContext context, CommandeService commandeService, UserService userService, ClientService clientService)
        {
            _context = context;
            _commandeService = commandeService;
            _userService = userService;
            _clientService = clientService;
        }

        // GET: Commande
        // public async Task<IActionResult> Index()
        // {
        //     // Chargement initial des commandes avec leurs relations
        //     IQueryable<Commande> applicationDbContext = _context.commande
        //         .Include(c => c.Client)
        //         .ThenInclude(c => c.users);

        //     // Filtrer les commandes pour un utilisateur avec le rôle CLIENT
        //     if (User.IsInRole("CLIENT"))
        //     {
        //         applicationDbContext = applicationDbContext.Where(c => c.Client.users.Login == User.Identity.Name);
        //     }

        //     // Récupérer les filtres de la requête
        //     string searchTerm = Request.Query["filter"]; // pour le checkbox
        //     string searchTerm1 = Request.Query["filter1"]; // pour le select

        //     // Gestion des états de filtre
        //     string filterTaken = searchTerm ?? "tous";
        //     string filterEtat = searchTerm1 ?? "tous";

        //     // Application des filtres
        //     if (filterTaken != "tous")
        //     {
        //         applicationDbContext = _commandeService.FindByTaken(applicationDbContext.ToList(), filterTaken).AsQueryable();
        //     }

        //     if (filterEtat != "tous")
        //     {
        //         applicationDbContext = _commandeService.FindByStatus(applicationDbContext.ToList(), filterEtat).AsQueryable();
        //     }


        //     // if (filterEtat == "envoye")
        //     // {
        //     //     applicationDbContext = applicationDbContext.Where(c => c.hasPayed == true);
        //     // }
        //     // else if (filterEtat == "nonenvoye")
        //     // {
        //     //     applicationDbContext = applicationDbContext.Where(c => c.hasPayed == false);
        //     // }

        //     // Exécuter la requête et passer les résultats à la vue
        //     var commandes = await applicationDbContext.ToListAsync();

        //     // Passer les termes de recherche au ViewBag pour conserver l'état des filtres
        //     ViewBag.searchTerm = filterTaken;
        //     ViewBag.searchTerm1 = filterEtat;

        //     return View(commandes);
        // }


        public async Task<IActionResult> Index()
        {
            var login = User.Identity.Name;
            var users = await _userService.FindByLogin(login);
            UserConnected userConnected = new UserConnected();
            userConnected.Login = users.Login;
            userConnected.Role = users.Role;
            userConnected.Nom = users.Nom;
            userConnected.Prenom = users.Prenom;
            userConnected.Telephone = users.telephone;
            if (users.Role == Role.CLIENT)
            {
                var client = await _clientService.FindByUserLogin(login);
                userConnected.Adresse = client.addresse;

            }
            ViewBag.UserConnected = userConnected;

            // Load initial query with relations
            IQueryable<Commande> applicationDbContext = _context.commande
                .Include(c => c.Client)
                .ThenInclude(c => c.users);

            // Filter orders for a user with CLIENT role
            if (User.IsInRole("CLIENT"))
            {
                applicationDbContext = applicationDbContext.Where(c => c.Client.users.Login == User.Identity.Name);
            }

            // Retrieve filters from the request
            string searchTerm = Request.Query["filter"]; // For checkbox
            string searchTerm1 = Request.Query["filter1"]; // For dropdown



            // Set default filter values if none provided
            string filterTaken = searchTerm ?? "tous";
            string filterEtat = searchTerm1 ?? "tous";

            Console.WriteLine("uysxdcfgvbhjn");
            Console.WriteLine(filterTaken);
            Console.WriteLine("uysxdcfgvbhjn,kljhgfdxswxfcghbjnk,ljhbgvfcdxfcghbjnk");

            // Apply filters directly on the IQueryable
            if (!string.IsNullOrEmpty(filterTaken) && filterTaken != "tous")
            {
                applicationDbContext = _commandeService.FindByTaken(applicationDbContext, filterTaken);
            }

            if (!string.IsNullOrEmpty(filterEtat) && filterEtat != "tous")
            {
                applicationDbContext = _commandeService.FindByStatus(applicationDbContext, filterEtat);
            }

            // Execute the query and pass results to the view
            var commandes = await applicationDbContext.ToListAsync();

            // Pass filter terms to the ViewBag to retain state
            ViewBag.searchTerm = filterTaken;
            ViewBag.searchTerm1 = filterEtat;

            return View(commandes);
        }



        // GET: Commande/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.commande
                .Include(c => c.Client)
                // .Include(c => c.Livreur)
                .Include(c => c.Client.users)

                .FirstOrDefaultAsync(m => m.Id == id);
            if (commande == null)
            {
                return NotFound();
            }
            ViewBag.produits = await _commandeService.GetProduitsCommande(commande.Id);
            ViewBag.Livreur = await _context.livreur.ToListAsync();

            return View(commande);
        }

        [Authorize(Roles = "RS")]
        [HttpPost]
        public async Task<IActionResult> Transfer()
        {
            var livreurId = Request.Form["livreurId"];
            var date = Request.Form["date"];
            var commandeId = Request.Form["commandeId"];
            var addresse = Request.Form["addresse"];
            var commande = await _commandeService.GetById(Convert.ToInt32(commandeId));
            if (commande == null)
            {
                // Handle the case where the commande is not found
                return NotFound();
            }
            var livreur = await _context.livreur.FirstOrDefaultAsync(l => l.Id == Convert.ToInt32(livreurId));
            Livraison livraison = new Livraison();
            livraison.commande = commande;
            livraison.livreur = livreur;
            livraison.addresse = addresse;
            livraison.dateLivraison = Convert.ToDateTime(date).ToUniversalTime();
            _context.Add(livraison);
            commande.Taken = true;
            await _commandeService.Update(commande);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> Create()
        {

            var produits = await _context.produit.ToListAsync();

            ViewBag.Produits = produits;

            var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier") ?? new Panier();

            if (panier != null)
            {
                HttpContext.Session.SetObjectAsJson("panier", panier);
            }
            ViewBag.panier = panier;
            return View();
        }


        public IActionResult AjouterProduit(int produitId, int qte)
        {

            var produit = _context.produit.FirstOrDefault(a => a.Id == produitId);
            var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier") ?? new Panier();
            panier.AjouterProduits(produit, qte);
            HttpContext.Session.SetObjectAsJson("panier", panier);

            return RedirectToAction("Create");
        }



        // [HttpPost]
        // public async Task<IActionResult> Create1()
        // {

        //     var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier") ?? new Panier();
        //     var commande = new Commande();
        //     double MontantTotal = 0.0;
        //     int disponible = 0;
        //     foreach (var item in panier.detailCommande)
        //     {
        //         if (item.qteCommande < item.produit.Quantite)
        //         {
        //             disponible = disponible + 1;
        //         }
        //     }
        //     foreach (DetailCommande detailCommande in panier.detailCommande)
        //     {


        //         detailCommande.commande = commande;
        //         detailCommande.produitId = detailCommande.produit.Id;
        //         _context.Attach(detailCommande.produit);
        //         _context.Add(detailCommande);

        //         MontantTotal += detailCommande.Prix;
        //     }

        //     if (panier.detailCommande.Count() == disponible)
        //     {
        //         commande.etatCommande = EtatCommande.NONMISENATTENTE;
        //     }

        //     Console.WriteLine("zaertyuiolmlkjhgfdsqzefg,");
        //     commande.DateCommande = DateTime.UtcNow;
        //     commande.UpdateAt = DateTime.UtcNow;
        //     commande.PrixTotal = MontantTotal;
        //     var client = _context.client.FirstOrDefault(a => a.users.Login == User.Identity.Name);
        //     commande.Client = client;
        //     _context.Add(commande);
        //     await _context.SaveChangesAsync();
        //     HttpContext.Session.Remove("panier");
        //     return RedirectToAction(nameof(Index));
        // }





        // GET: Commande/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.commande.FindAsync(id);
            if (commande == null)
            {
                return NotFound();
            }
            ViewData["CLientId"] = new SelectList(_context.client, "Id", "Id", commande.CLientId);
            // ViewData["LivreurId"] = new SelectList(_context.livreur, "Id", "Id", commande.LivreurId);
            return View(commande);
        }

        // public ActionResult Deletedetail(int? id)
        // {
        //     if (id == null)
        //     {
        //         return BadRequest("ID is required.");
        //     }

        //     var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier");
        //     if (panier == null)
        //     {
        //         return NotFound("Panier not found in session.");
        //     }

        //     panier.deleteDetail(id.Value);

        //     // Mettre à jour le panier en session après modification
        //     HttpContext.Session.SetObjectAsJson("panier", panier);

        //     return RedirectToAction("Index", "Achat"); // Ou l'action appropriée
        // }


        // POST: Commande/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DateCommande,PrixTotal,CLientId,LivreurId,hasPayed,Taken,Id,CreateAt,UpdateAt")] Commande commande)
        {
            if (id != commande.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeExists(commande.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CLientId"] = new SelectList(_context.client, "Id", "Id", commande.CLientId);
            // ViewData["LivreurId"] = new SelectList(_context.livreur, "Id", "Id", commande.LivreurId);
            return View(commande);
        }




        // GET: Commande/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.commande
                .Include(c => c.Client)
                // .Include(c => c.Livreur)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // POST: Commande/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commande = await _context.commande.FindAsync(id);
            if (commande != null)
            {
                _context.commande.Remove(commande);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeExists(int id)
        {
            return _context.commande.Any(e => e.Id == id);
        }
    }
}
