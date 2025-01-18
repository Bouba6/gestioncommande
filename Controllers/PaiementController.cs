using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using gestioncommande.Models;
using gestioncommande.Services;
using gestioncommande.Enum;
using gestioncommande.Services.Impl;
using Microsoft.AspNetCore.Authorization;
namespace gestioncommande.Controllers
{
    [Authorize(Roles = "CLIENT")]
    public class PaiementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CommandeService _commandeService;
        private readonly ClientService _clientService;

        public PaiementController(ApplicationDbContext context, CommandeService commandeService, ClientService clientService)
        {

            _context = context;
            _commandeService = commandeService;
            _clientService = clientService;

        }

        // GET: Paiement
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.paiement.Include(p => p.commande);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Paiement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paiement = await _context.paiement
                .Include(p => p.commande)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paiement == null)
            {
                return NotFound();
            }

            return View(paiement);
        }

        // GET: Paiement/Create
        public IActionResult Create()
        {
            ViewData["commandeId"] = new SelectList(_context.commande, "Id", "Id");
            return View();
        }

        // POST: Paiement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> paiement()
        {
            Paiement paiement = new Paiement();
            var commandeId = Request.Form["commandeId"];
            var reference = Request.Form["reference"];
            var commande = await _commandeService.GetById(Convert.ToInt32(commandeId));
            var client = await _clientService.GetById(commande.CLientId);
            if (commande == null)
            {

                return NotFound();
            }



            // if (commande.Client == null)
            // {
            //     return NotFound();
            // }
            client.Solde = client.Solde - commande.PrixTotal;

            paiement.commande = commande;
            TypePaiement typePaiement = (TypePaiement)int.Parse(Request.Form["paymentMethod"]);
            paiement.TypePaiement = typePaiement;
            paiement.reference = reference;
            commande.hasPayed = true;
            if (commande.Client == null)
            {
                Console.WriteLine("passsssssssssssssssssssss maaaaaaaaaaaaaaaaaaaa");
                return NotFound();
            }

            if (client == null)
            {
                Console.WriteLine("passsssssssssssssssssssss maaaaaaaaaaaaaaaaaaaa");
                return NotFound();
            }

            await _clientService.Update(client);
            _context.Add(paiement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Commande");


        }

        // GET: Paiement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paiement = await _context.paiement.FindAsync(id);
            if (paiement == null)
            {
                return NotFound();
            }
            ViewData["commandeId"] = new SelectList(_context.commande, "Id", "Id", paiement.commandeId);
            return View(paiement);
        }

        // POST: Paiement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypePaiement,commandeId,hasReduced,Id,CreateAt,UpdateAt")] Paiement paiement)
        {
            if (id != paiement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paiement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaiementExists(paiement.Id))
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
            ViewData["commandeId"] = new SelectList(_context.commande, "Id", "Id", paiement.commandeId);
            return View(paiement);
        }

        // GET: Paiement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paiement = await _context.paiement
                .Include(p => p.commande)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paiement == null)
            {
                return NotFound();
            }

            return View(paiement);
        }

        // POST: Paiement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paiement = await _context.paiement.FindAsync(id);
            if (paiement != null)
            {
                _context.paiement.Remove(paiement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaiementExists(int id)
        {
            return _context.paiement.Any(e => e.Id == id);
        }
    }
}
