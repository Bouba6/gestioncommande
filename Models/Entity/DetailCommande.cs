using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace gestioncommande.Models;
public class DetailCommande : AbstractEntity
{
    public double qteCommande { get; set; }
    [NotMapped]
    public int IdTemporaire { get; set; } // ID temporaire
    public Commande commande { get; set; }

    public int commandeId { get; set; }

    public double Prix { get; set; }

    public Produit produit { get; set; }

    public int produitId { get; set; }
}
