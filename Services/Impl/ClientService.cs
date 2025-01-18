using gestioncommande.Models;
using Microsoft.EntityFrameworkCore;
using gestioncommande.Services;

namespace gestioncommande.Services.Impl;

public class ClientService : IClientService
{

    private readonly ApplicationDbContext _context;
    public ClientService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Client> Create(Client client)
    {

        _context.client.Add(client);

        await _context.SaveChangesAsync();

        return client;
    }

    public async Task<Client> Delete(int id)
    {
        var client = await _context.client.FindAsync(id);
        _context.client.Remove(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> Update(Client client)
    {
        client.UpdateAt = DateTime.UtcNow;
        _context.client.Update(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> GetById(int id)
    {
        return await _context.client.FindAsync(id);
    }



    public async Task<IEnumerable<Client>> GetClientsAsync()
    {
        return await _context.client.ToListAsync();
    }

    public Task<Client> FindByUserLogin(string login)
    {
        return _context.client.FirstOrDefaultAsync(u => u.users.Login == login);
    }
}