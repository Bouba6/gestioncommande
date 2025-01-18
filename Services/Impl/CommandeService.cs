using gestioncommande.Models;
using Microsoft.EntityFrameworkCore;
using gestioncommande.Services;
namespace gestioncommande.Services.Impl;

public class CommandeService : ICommandeService
{
    private readonly ApplicationDbContext _context;

    public CommandeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Commande>> GetCommandesAsync()
    {
        return await _context.commande.ToListAsync();
    }

    public async Task<Commande> GetById(int id)
    {
        return await _context.commande.FindAsync(id);
    }

    public async Task<Commande> Update(Commande commande)
    {
        commande.UpdateAt = DateTime.UtcNow; //DateTime.UtcNow;
        _context.commande.Update(commande);
        await _context.SaveChangesAsync();
        return commande;
    }


    public async Task<Commande> Create(Commande commande)
    {

        _context.commande.Add(commande);

        await _context.SaveChangesAsync();

        return commande;
    }

    public IQueryable<Commande> FindByStatus(IQueryable<Commande> commandes, string status)
    {
        if (status == "tous")
        {
            return commandes; // No filter, return the original IQueryable
        }
        else if (status == "true")
        {
            return commandes.Where(c => c.hasPayed == true); // Filter based on 'Taken'
        }
        else
        {
            return commandes.Where(c => c.hasPayed == false);
        }
    }

    public IQueryable<Commande> FindByTaken(IQueryable<Commande> commandes, string status)
    {
        if (status == "tous")
        {
            return commandes; // No filter, return the original IQueryable
        }
        else if (status == "true")
        {
            return commandes.Where(c => c.Taken == true); // Filter based on 'Taken'
        }
        else
        {
            return commandes.Where(c => c.Taken == false);
        }
    }


    public async Task<IEnumerable<DetailCommande>> GetProduitsCommande(int id)
    {
        return await _context.detailCommande.Where(dc => dc.commandeId == id).Include(dc => dc.produit).ToListAsync();
    }


}