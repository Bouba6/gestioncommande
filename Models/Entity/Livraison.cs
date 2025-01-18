using System.ComponentModel.DataAnnotations.Schema;

namespace gestioncommande.Models;
public class Livraison : AbstractEntity
{

    public Commande commande { get; set; }
    public int commandeId { get; set; }

    public DateTime dateLivraison { get; set; }

    public string addresse { get; set; }

    public Livreur livreur { get; set; }

    public int LivreurId { get; set; }

}