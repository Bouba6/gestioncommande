using gestioncommande.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace gestioncommande.Models;
public class UserConnected
{
    public string? Nom { get; set; }
    public string? Prenom { get; set; }

    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }

    public string? Telephone { get; set; }

    public string? Adresse { get; set; }
    public Role Role { get; set; }
}