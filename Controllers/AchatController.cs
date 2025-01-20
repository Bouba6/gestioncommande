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
    public class AchatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AchatController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {


            var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier");
            // Pass the updated panier to the view
            ViewBag.panier = panier;

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Create1()
        {

            var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier");
            double MontantTotal = 0.0;

            if (panier == null || !panier.detailCommande.Any())
            {

                TempData["ErrorMessage"] = "Votre panier est vide.";
                return RedirectToAction("Index", "Panier");
            }


            var login = User.Identity.Name;
            var user = _context.users.FirstOrDefault(u => u.Login == login);


            if (user == null)
            {
                TempData["ErrorMessage"] = "Utilisateur non trouvé.";
                // return NotFound();

                //return RedirectToAction("Login", "Account");
            }

            var client = _context.client.FirstOrDefault(c => c.users.Login == user.Login);
            client.Solde += panier.PrixTotal;

            if (client == null)
            {
                TempData["ErrorMessage"] = "Client non trouvé.";
                Console.WriteLine("Client non trouvé. zertyuhjhgfdsqERTYUIOUYTRESQZERTYU");
                // return NotFound();
            }
            int disponible = 0;

            foreach (var item in panier.detailCommande)
            {
                if (item.qteCommande < item.produit.Quantite)
                {
                    disponible += 1;
                }
            }

            Commande commande = new Commande
            {
                Client = client,
                CreateAt = DateTime.UtcNow,
                DateCommande = DateTime.UtcNow,
                etatCommande = EtatCommande.MISENATTENTE,
                hasPayed = false,
                Taken = false
            };

            if (panier.detailCommande.Count() == disponible)
            {
                commande.etatCommande = EtatCommande.NONMISENATTENTE;
            }


            foreach (var detailsCommande in panier.detailCommande)
            {
                _context.Attach(detailsCommande.produit);
                _context.Add(detailsCommande);
                // MontantTotal += detailsCommande.Prix;
                MontantTotal = panier.PrixTotal;
                detailsCommande.commandeId = commande.Id;
                commande.detailCommandes.Add(detailsCommande);
            }

            // client.Commandes.Add(commande);
            client.Solde += MontantTotal;

            commande.PrixTotal = MontantTotal;
            _context.commande.Add(commande);
            _context.client.Update(client);



            await _context.SaveChangesAsync();

            // Vider le panier après la création de la commande
            HttpContext.Session.Remove("panier");

            // Rediriger vers une page de confirmation ou la liste des commandes
            TempData["SuccessMessage"] = "Votre commande a été créée avec succès!";
            return RedirectToAction("Index", "Commande");
        }




        public ActionResult Deletedetail(int? id)
        {
            if (id == null)
            {
                return BadRequest("ID is required.");
            }

            var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier");
            if (panier == null)
            {
                return NotFound("Panier not found in session.");
            }

            panier.deleteDetail(id.Value);

            // Mettre à jour le panier en session après modification
            HttpContext.Session.SetObjectAsJson("panier", panier);
            return RedirectToAction("Index", "Achat");

        }







        [HttpPost]
        public IActionResult UpdateQuantityInSession([FromBody] DetailCommande model)
        {

            var login = User.Identity.Name;
            var user = _context.users.FirstOrDefault(u => u.Login == login);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Utilisateur non trouvé.";
                return RedirectToAction("Login", "Account");
            }

            var client = _context.client.FirstOrDefault(c => c.users.Login == user.Login);
            var panier = HttpContext.Session.GetObjectFromJson<Panier>("panier");

            if (panier != null)
            {
                var item = panier.detailCommande.FirstOrDefault(i => i.IdTemporaire == model.Id);
                if (item != null)
                {
                    item.qteCommande = model.qteCommande;
                    panier.AjouterProduits(item.produit, item.qteCommande);
                }




                var counting = 0;
                List<Commande> commandes = _context.commande.ToList();
                foreach (var commande in commandes)
                {
                    if (commande.CLientId == client.Id && commande.hasPayed == true)
                    {
                        counting++;
                    }
                }

                Console.WriteLine($"Count of paid commandes: {counting}");

                if (counting >= 10)
                {
                    Console.WriteLine(" je suis rentreeeeeeeeeeeeeeeeeeePrixTotalAvecreduction: xc vbn,;jnhbgfvcxdsw<qwsxdfcvghbjnbvfcxdswq<wsdxc vbn");
                    panier.PrixTotalAvecreduction = 1;

                }


                panier.PrixTotalCommande();









                // Recalculer le total après la mise à jour de la quantité


                // Sauvegarder le panier mis à jour dans la session
                HttpContext.Session.SetObjectAsJson("panier", panier);

                // Retourner le nouveau total au client
                return Json(new { success = true, newTotal = panier.PrixTotal, value = panier.PrixTotalAvecreduction });
            }

            return Json(new { success = false });
        }

    }
}