using gestioncommande.Models;
namespace gestioncommande.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetClientsAsync();
    Task<User> Create(User user);
    Task<User> Update(User user);

    Task<User> FindByLogin(string login);

}