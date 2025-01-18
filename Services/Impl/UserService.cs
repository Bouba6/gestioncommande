
using gestioncommande.Enum;
using gestioncommande.Models;
using gestioncommande.Services;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{


    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<User> Create(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByLogin(string login)
    {

        return _context.users.FirstOrDefaultAsync(u => u.Login == login);
    }

    public Task<IEnumerable<User>> GetClientsAsync()
    {
        return _context.users
            .Where(u => u.Role == Role.CLIENT)
            .ToListAsync()
            .ContinueWith(t => t.Result.AsEnumerable());
    }
    public Task<User> Update(User user)
    {
        throw new NotImplementedException();
    }
}