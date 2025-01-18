using gestioncommande.Models;
namespace gestioncommande.Services;

public interface IProduitService
{
    Task<IEnumerable<Produit>> GetProduitsAsync();
    Task<Produit> Create(Produit produit);
    Task<Produit> Update(Produit produit);
    Task<Produit> GetById(int id);
    Task<Produit> Delete(int id);

}