
using gestioncommande.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace gestioncommande.Models;

public class UserDto : AbstractEntity
{

    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string telephone { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public double Solde { get; set; }
    public string Adresse { get; set; }
    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
    public string ConfirmPassword { get; set; }
}