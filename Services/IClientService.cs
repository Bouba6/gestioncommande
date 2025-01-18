using gestioncommande.Models;
namespace gestioncommande.Services;
public interface IClientService
{
    Task<Client> Create(Client client);
    Task<Client> Update(Client client);
    Task<Client> Delete(int id);
    Task<Client> GetById(int id);
    Task<IEnumerable<Client>> GetClientsAsync();

    Task<Client> FindByUserLogin(string login);
}