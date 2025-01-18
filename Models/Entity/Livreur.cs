using System.ComponentModel.DataAnnotations.Schema;

namespace gestioncommande.Models;
public class Livreur : Personne
{
    public bool estDisponible { get; set; } = true;



    [NotMapped]
    public List<Commande> Commandes { get; set; }
}
