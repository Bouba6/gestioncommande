

using gestioncommande.Models;
using gestioncommande.Services;
using Microsoft.EntityFrameworkCore;

public class ProduitService : IProduitService
{


    private readonly ApplicationDbContext _context;

    public ProduitService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produit>> GetProduitsAsync()
    {
        return await _context.produit.ToListAsync();
    }
    public async Task<Produit> Create(Produit produit)
    {

        _context.produit.Add(produit);

        await _context.SaveChangesAsync();

        return produit;
    }

    public async Task<Produit> Update(Produit produit)
    {
        produit.UpdateAt = DateTime.UtcNow;
        _context.produit.Update(produit);
        await _context.SaveChangesAsync();
        return produit;
    }

    public async Task<Produit> Delete(int id)
    {
        var produit = await _context.produit.FindAsync(id);
        _context.produit.Remove(produit);
        await _context.SaveChangesAsync();
        return produit;
    }

    public async Task<Produit> GetById(int id)
    {
        return await _context.produit.FindAsync(id);
    }


}