using gestioncommande.Models;
namespace gestioncommande.Services;

public interface IPersonneService
{
    Task<IEnumerable<Personne>> GetClientsAsync();
    Task<User> Create(Personne personne);
    Task<User> Update(Personne personne);

}