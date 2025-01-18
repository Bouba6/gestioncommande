using gestioncommande.Models;
namespace gestioncommande.Services;
public interface ICommandeService
{
    Task<IEnumerable<Commande>> GetCommandesAsync();
    Task<Commande> Create(Commande commande);

    Task<Commande> Update(Commande commande);

    Task<Commande> GetById(int id);

    IQueryable<Commande> FindByStatus(IQueryable<Commande> commandes, string status);

    IQueryable<Commande> FindByTaken(IQueryable<Commande> commandes, string status);

    Task<IEnumerable<DetailCommande>> GetProduitsCommande(int id);

}